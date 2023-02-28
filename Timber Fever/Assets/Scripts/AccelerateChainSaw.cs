using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AccelerateChainSaw : MonoBehaviour
{

    //Chainsaw � double click ile h�zland�rma.

    private float pastTime;
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;
    public GameObject mainCam;
    public GameObject vfx;

    public bool isMoving = false;
    public bool isMovingBack = false;

    // Update is called once per frame
    void Update()
    {
        //Double click check
        if (!isMoving)
        {
            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                leftClickNum += 1;
                
            }
            if (leftClickNum == 1 && isTimeCheckAllowed)
            {
                firstLeftClickTime = Time.time;
                StartCoroutine(DetectDoubleLeftClick());
            }
        }
        
        //Double click check
        IEnumerator DetectDoubleLeftClick()
        {
            isTimeCheckAllowed = false;
            while (Time.time < firstLeftClickTime + timeBetweenLeftClick)
            {
                //Double click ger�ekle�ince
                if (leftClickNum == 2)
                {
                    //Testere ve a�a� h�z� set ediliyor
                    this.GetComponent<GameManager>().speedMultiply = 2;
                    mainCam.GetComponent<AccelCamMovement>().MoveCamToLastPos();
                    isMoving = true;
                    isMovingBack = true;
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
            leftClickNum = 0;
            isTimeCheckAllowed = true;

        }

        //Double click ge�en s�re
        pastTime = Time.time - firstLeftClickTime;

        //Ge�en s�re 5 den b�y�kse ve art�k kamera hareketi tamamland�ysa
        if(pastTime > 5f && isMovingBack)
        {
            //Testere ve a�a� h�z� eski haline d�nd�r�l�yor
            this.GetComponent<GameManager>().speedMultiply = 1;
            //Kamera ilk konuma hareket ettiriliyor.
            mainCam.GetComponent<AccelCamMovement>().MoveCamToFirstPos();
            isMovingBack = false;
        }

    }


    

}
