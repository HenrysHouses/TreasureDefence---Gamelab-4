using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFloater : MonoBehaviour
{
    // int Index;
    public float offset;
    // public float strength = 0.6f;

    float lastYPos;
    Rigidbody _rigidbody;
    // [SerializeField] float rotateStrength = 0.02f; 

    // [SerializeField] Transform[] floaters;
    
    Vector3 vert;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        // offset = transform.position.y - 0.087f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector4 vertPos = FloaterManager.instance.getClosestVert(transform, ref FloaterManager.instance._index);
        // vertPos = FloaterManager.instance.transform.TransformPoint(vertPos);
        
        Vector3 newPos = new Vector3(transform.position.x, (vertPos.y*-1) + offset, transform.position.z);
        transform.position = newPos;

        // foreach (var floater in floaters)
        // {
            // float distance = Vector3.Distance(vertPos, transform.position);
            // float difference = Mathf.Clamp01(distance / offset) * strength; // Vector3.Distance(vertPos, floater.position);
            // vert = vertPos;
            // if(floater.position.y < vertPos.y)
            // {
            //     Vector3 force = new Vector3(0f, Mathf.Abs(Physics.gravity.y) * difference, 0f);
            //     Debug.Log(floater.name + ": " + force);
            //     _rigidbody.AddForceAtPosition(force, floater.position, ForceMode.Acceleration);
            // }
            // Vector3 euler = transform.eulerAngles;

            // float z = Mathf.Clamp(euler.z, 140, 220);

            // euler = new Vector3(euler.x, euler.y, z);
            // transform.eulerAngles = euler;

        // }
       
        // vertPos *= -1;
        // transform.position = new Vector3(transform.position.x, offset + vertPos.y * strength, transform.position.z);

       

        lastYPos = transform.position.y;
        // transform.position = pos;
    }
}
