using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawrotate : MonoBehaviour
{
    private int rotationSpeed = 300;

    private void FixedUpdate()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
    }
}
