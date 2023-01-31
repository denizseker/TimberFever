using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private float DestroyTime = 2f;
    private Vector3 OffSet = new Vector3(0,3.5f,0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.localPosition += OffSet;

    }

}
