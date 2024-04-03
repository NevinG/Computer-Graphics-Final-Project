using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YOffset : MonoBehaviour
{
    public float yOffset;
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
    }
}
