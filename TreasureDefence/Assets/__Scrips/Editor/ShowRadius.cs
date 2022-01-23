using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRadius : MonoBehaviour
{
    public float radius;

    private void OnDrawGizmosSelected()
    {
        MyGizmofs.DrawWireCircle(transform.position, Quaternion.identity, radius);
    }
}
