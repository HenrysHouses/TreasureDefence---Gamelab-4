using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTransform : MonoBehaviour
{
    public Transform t;
    
    void FixedUpdate()
    {
        transform.LookAt(t);
    }
}
