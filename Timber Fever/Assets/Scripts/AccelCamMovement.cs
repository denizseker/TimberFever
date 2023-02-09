using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelCamMovement : MonoBehaviour
{

    //Double click ile chainsaw� h�zland�r�rken kamera pozisyonu ayarlama


    public GameObject gameManager;
    //H�zlanma kameras� i�in eklenecek kordinatlar.
    private Vector3 accelPos = new Vector3(0,4,-4);
    private Vector3 offSet = new Vector3(0.05f, 0.05f, -0.05f);
    private Vector3 lastPos = new Vector3(100,100,100);
    private Vector3 firstPos = new Vector3(100,100,100);
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
        //Kamera h�zland���nda olacak konuma getiriliyor.
        if (isMoveLast)
        {
            transform.position = Vector3.Lerp(transform.position, lastPos, 3 * Time.deltaTime);
        }
        //Kamera h�zlanman�n son konumunda
        if(transform.position.y > lastPos.y - offSet.y && isMoveLast)
        {
            isMoveLast = false;
        }

        //Kamera h�zlanmadan �nceki konuma getiriliyor.
        if (isMoveFirst)
        {
            transform.position = Vector3.Lerp(transform.position, firstPos, 3 * Time.deltaTime);
        }
        //Kamera ilk konumda
        if (transform.position.y < firstPos.y + offSet.y && isMoveFirst)
        {
            //H�zland�rman�n bitti�i s�yleniyor.
            gameManager.GetComponent<AccelerateChainSaw>().isMoving = false;
            isMoveFirst = false;
        }
    }
}
