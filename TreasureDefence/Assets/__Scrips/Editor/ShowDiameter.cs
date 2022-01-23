using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShowDiameter : MonoBehaviour
{
    public float radius;

    private void OnDrawGizmosSelected()
    {
        MyGizmofs.DrawWireCircle(transform.position, Quaternion.identity, radius);
    }
}
