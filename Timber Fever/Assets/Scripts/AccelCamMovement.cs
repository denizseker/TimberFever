using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelCamMovement : MonoBehaviour
{

    private Vector3 accelPos = new Vector3(0,3,-3);
    private Vector3 lastPos;
    private Vector3 firstPos;
    private bool isMoveLast = false;
    private bool isMoveFirst = false;

    public void MoveCamToLastPos()
    {
        firstPos = transform.position;
        lastPos = transform.position + accelPos;
        isMoveLast = true;
    }
    public void MoveCamToFirstPos()
    {
        firstPos = transform.position - accelPos;
        isMoveFirst = true;
    }


    private void FixedUpdate()
    {
        if (isMoveLast)
        {
            transform.position = Vector3.Lerp(transform.position, lastPos, 3 * Time.deltaTime);
        }
        if(transform.position == lastPos)
        {
            isMoveLast = false;
        }

        if (isMoveFirst)
        {
            transform.position = Vector3.Lerp(transform.position, firstPos, 3 * Time.deltaTime);
        }
        if (transform.position == firstPos)
        {
            isMoveFirst = false;
        }

        
    }
}
