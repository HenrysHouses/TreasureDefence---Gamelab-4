using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovesItems : MonoBehaviour
{
    public Vector3 moveDirection;
    
    private List<Transform> items = new List<Transform>();

    private void Update()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].position += moveDirection * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        items.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        items.Remove(other.transform);
    }
}
