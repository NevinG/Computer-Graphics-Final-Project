using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYaxis : MonoBehaviour
{
    public float yRotation;
    void Start()
    {
        // Instantly rotate the object to 90 degrees around the Y-axis
        transform.eulerAngles = new Vector3(0f, 0f + yRotation, 0f);
    }
}
