using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInRiver : MonoBehaviour
{
    public PathController pc;

    public OrientedPoint op;
    
    public bool isOnPoint;

    public bool isInRiver;
    
    private Vector3 point;

    public Vector3 moveDirection;
    
    private void FixedUpdate()
    {
        if (isInRiver)
        {
            if (isOnPoint)
            {
                transform.position += moveDirection * Time.fixedDeltaTime;
                /*
                            progress += Time.deltaTime;
        
                            op = path.GetPathOP(enemyStats.speed * progress);

                            transform.position = op.pos;
                 */
            }
            else
            {
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //op = pc.getClosestOP(transform, );

        isInRiver = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isInRiver = false;
    }
}
