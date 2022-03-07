using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public float speed;
    

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, speed * Time.deltaTime);    
    }
}
