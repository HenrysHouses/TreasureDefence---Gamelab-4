using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleData : MonoBehaviour
{
    public bool isHit;
    bool isMovingDown, isMovingUp;
    public bool isMoving => isMovingDown || isMovingUp;
    public float speed;
    public float heightOffset;
    public float cooldown;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        OnHit();    
    }

    void OnHit()
    {

    }

    public void showMole()
    {
        Vector3 pos = transform.position; 

        transform.position = new Vector3(pos.x, pos.y + speed, pos.z);
    }
}
