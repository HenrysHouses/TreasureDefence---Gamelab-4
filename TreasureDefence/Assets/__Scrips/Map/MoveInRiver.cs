using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInRiver : MonoBehaviour
{
    public PathController pc;

    OrientedPoint op;

    public bool isOnPoint;

    public bool isInRiver;

    //private Vector3 point;

    public Vector3 moveDirection;

    private int point;
    private float offset;

    public float speed;

    private Rigidbody rb;

    private void Start()
    {
        pc = GameManager.instance.pathController;
    }

    private void FixedUpdate()
    {
        if (isInRiver)
        {
            offset += Time.fixedDeltaTime * speed;
            
            op = pc.GetEvenPathOP(offset);

            transform.position = op.pos;

            if (offset >= 1)
            {
                isInRiver = false;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("River") && op != null)
        {
            op = pc.getClosestOP(transform, ref point);

            offset = pc.GetEvenPathTOffset(point);

            op = pc.GetEvenPathOP(offset);

            //op = pc.getClosestOP(transform, );


            isInRiver = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("River"))
        {
            isInRiver = false;

            offset = 0;    
        }
    }
}