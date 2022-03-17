using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_alwaysFacePlayer : MonoBehaviour
{
    [SerializeField] bool keepYRotation;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);        
        if(keepYRotation)
        {
            Vector3 eulers = transform.eulerAngles;
            eulers.x = 0;
            eulers.z = 0;
            transform.eulerAngles = eulers;
        }
    }
}
