using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleBob : MonoBehaviour
{
    private float originalHeight;

    private float value;
    
    public float speed;

    private float originalSpeed;
    
    public float height;

    public float startOffset;
    
    private void Start()
    {
        value = 0f;
        
        originalHeight = transform.position.y;

        originalSpeed = speed;
        speed = 0f;
        
        Invoke(nameof(Begin), startOffset);
    }

    void Update()
    {
        value += speed * Time.deltaTime;
        
        transform.position = new Vector3(transform.position.x, originalHeight + Mathf.PingPong(value, height), transform.position.z);
    }

    private void Begin()
    {
        speed = originalSpeed;
    }
}
