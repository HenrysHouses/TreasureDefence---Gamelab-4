using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOffset : MonoBehaviour
{
    [SerializeField] float rotateOffset;
    void LateUpdate()
    {
        Vector3 rotateBy = new Vector3(rotateOffset, 0, 0);
        transform.Rotate(rotateBy, Space.Self);
    }
}
