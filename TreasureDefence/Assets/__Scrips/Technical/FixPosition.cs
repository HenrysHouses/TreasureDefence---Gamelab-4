using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPosition : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRot;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition != startPos)
        {
            transform.localPosition = startPos;
            transform.rotation = startRot;
        }
    }
}
