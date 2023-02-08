using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AccelerateChainSaw : MonoBehaviour
{

    public Text text1;
    public Text text2;

    private float pastTime;
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;
    public GameObject mainCam;

    public bool deneme = false;

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            leftClickNum += 1;
        }
        if(leftClickNum == 1 && isTimeCheckAllowed)
        {
            firstLeftClickTime = Time.time;
            StartCoroutine(DetectDoubleLeftClick());
        }

        IEnumerator DetectDoubleLeftClick()
        {
            isTimeCheckAllowed = false;
            while(Time.time < firstLeftClickTime + timeBetweenLeftClick)
            {
                if(leftClickNum == 2)
                {
                    mainCam.GetComponent<AccelCamMovement>().MoveCamToLastPos();
                    deneme = true;
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
            leftClickNum = 0;
            isTimeCheckAllowed = true;
        }

        pastTime = Time.time - firstLeftClickTime;

        //Debug.Log("Past time:" + pastTime);

        if(pastTime > 5f && deneme)
        {
            Debug.Log("5i geçti");
            mainCam.GetComponent<AccelCamMovement>().MoveCamToFirstPos();
            deneme = false;
        }

    }


    

}
