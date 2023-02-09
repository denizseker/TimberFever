using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AccelerateChainSaw : MonoBehaviour
{

    //Chainsaw ý double click ile hýzlandýrma.

    public Text text1;
    public Text text2;

    private float pastTime;
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;
    public GameObject mainCam;

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
                Debug.Log("Leftclick num: " + leftClickNum);
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
                //Double click gerçekleþince
                if (leftClickNum == 2)
                {
                    //Testere ve aðaç hýzý set ediliyor
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

        //Double click geçen süre
        pastTime = Time.time - firstLeftClickTime;

        //Geçen süre 5 den büyükse ve artýk kamera hareketi tamamlandýysa
        if(pastTime > 5f && isMovingBack)
        {
            //Testere ve aðaç hýzý eski haline döndürülüyor
            this.GetComponent<GameManager>().speedMultiply = 1;
            //Kamera ilk konuma hareket ettiriliyor.
            mainCam.GetComponent<AccelCamMovement>().MoveCamToFirstPos();
            isMovingBack = false;
        }

    }


    

}
