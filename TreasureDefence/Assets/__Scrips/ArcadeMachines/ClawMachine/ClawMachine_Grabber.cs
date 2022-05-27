using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMachine_Grabber : MonoBehaviour
{
    public Transform grabTarget, item, itemParent, grabParent;

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CrabGrabber") && grabTarget == null)
        {
            grabTarget = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        grabTarget = null;
    }

    public void grab()
    {
        if(!grabTarget)
            return;
        if(Vector3.Distance(grabTarget.position, transform.position) > 0.01)
        {
            if(grabTarget)
            {
                item = grabTarget;
                item.SetParent(grabParent);
                Rigidbody rb = item.GetComponent<Rigidbody>();
                if(rb)
                {
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    rb.useGravity = false;
                }
                item.localPosition = new Vector3();
            }
        }
    }

    public void drop()
    {
        if(item)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if(rb)
            {
                rb.constraints = RigidbodyConstraints.None;
                    rb.useGravity = true;
            }
            item.SetParent(itemParent);
            item = null;
        }
    }
}