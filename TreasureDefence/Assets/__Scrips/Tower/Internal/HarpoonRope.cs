using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonRope : MonoBehaviour
{
    public LineRenderer lineRenderer; 
    public Transform harpoonRopePos;

    // Update is called once per frame
    void Update()
    {
        if(lineRenderer.GetPosition(1) != harpoonRopePos.position)
           lineRenderer.SetPosition(1, harpoonRopePos.position);
        if(lineRenderer.GetPosition(0) != transform.position)
           lineRenderer.SetPosition(0, transform.position);
    }
}
