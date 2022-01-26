using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool held;
    
    public GameObject obj;

    public void SetHeld(bool hold)
    {
        held = hold;
    }

    private void FixedUpdate()
    {
        if (held)
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position - (Vector3.down * 0.1f), Vector3.down, Color.red);
            
            if (Physics.Raycast(transform.position - (Vector3.down * 0.1f), Vector3.down, out hit, 2f))
            {
                
                
                Debug.Log("Triggered");
                
                if (!obj.activeSelf)
                    obj.SetActive(true);

                obj.transform.position = hit.point;
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
}
