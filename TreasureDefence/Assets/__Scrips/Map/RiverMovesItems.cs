using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class RiverMovesItems : MonoBehaviour
{
    public Vector3 moveDirection;
    
    private List<Transform> items = new List<Transform>();

    private bool hasObjects = false;

    private void OnTriggerEnter(Collider other)
    {
        items.Add(other.transform);

        if (!hasObjects)
        {
            InvokeRepeating(nameof(MoveObjects), 0.0f, 0.02f);
            
            hasObjects = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        items.Remove(other.transform);

        if (items.Count == 0)
        {
            CancelInvoke();

            hasObjects = false;
        }
    }

    private void MoveObjects()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].position += moveDirection * Time.fixedDeltaTime;
        }
    }
}
