using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZOffset : MonoBehaviour
{
    public float zOffset;
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + zOffset);
    }
}
