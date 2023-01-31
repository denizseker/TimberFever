using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteArea : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "tree")
        {
            Destroy(other.gameObject);
        }
    }
}
