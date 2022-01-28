using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShowDiameter : MonoBehaviour
{
    public float radius;

    public bool _enabled;
    
    void OnDrawGizmosSelected()
    {
        //MyGizmofs.DrawWireCircle(transform.position, Quaternion.LookRotation(transform.right), radius);
        
        if (_enabled)
            Gizmos.DrawSphere(transform.position, radius);
    }
}
