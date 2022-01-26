using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTransform : MonoBehaviour
{
    public static RotateTowardsTransform instance;

    private bool anim = false;
    
    public Transform player;

    public float speed;
    private float t;
    

    public Quaternion originalRotation;
    public Quaternion toRotation;
    
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        // Debug.Log(toRotation);
    }

    void FixedUpdate()
    {
        if (!anim)
            transform.LookAt(player);
        else
        {
            /*
            if (t < 1)            
                t += Time.fixedDeltaTime * speed;
            else if (t > 0)
                t -= Time.fixedDeltaTime * speed;
            */

            
            t += Time.fixedDeltaTime * speed;

            
            float tpos = 0;

            if (t < 1)
                tpos = t;
            
            if (t > 1)
                tpos = ExtensionMethods.Remap(t, 1, 2, 1, 0);
            
            
            
            transform.rotation = Quaternion.Lerp(originalRotation, toRotation, tpos);
            
            
        }
    }

    public void DoBuyAnimation()
    {
        anim = true;

        t = 0;
        
        Invoke(nameof(StopBuyAnim), 1f);
    }

    private void StopBuyAnim()
    {
        anim = false;
    }
}
