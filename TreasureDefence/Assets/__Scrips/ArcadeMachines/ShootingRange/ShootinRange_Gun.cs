using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootinRange_Gun : TD_Interactable
{   //Mikkel & Henrik, (Mostly HenriK).

    public GameObject Gun;
    [SerializeField]
    ShootingRange shootingrange;
    PlayerInteraction player;
    float timer;
    public float ReloadTime;

    /// <summary>Called at the start of an interaction</summary>
    /// <param name="target">Pass any object data through</param>
    override public void InteractTrigger(object target = null)
    {
        player = target as PlayerInteraction;

        SetHeld(true, player.GetHoldPoint);
    }

    private new void Update()
    {
        base.Update();

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //if the target is hit 5 times within ? many seconds.
        timer -= Time.deltaTime;

        if(Input.GetMouseButtonDown(0) && timer <= 0)
        {
            timer = ReloadTime;

            Debug.DrawRay(Gun.transform.position, Gun.transform.forward);

            if (Physics.Raycast(Gun.transform.position, -Gun.transform.forward, out hit))
            {
                
                if (hit.transform.CompareTag("Target"))
                {
                    shootingrange.HitTarget++;
                    Debug.Log("hit = " + shootingrange.HitTarget);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            SetHeld(false, player.GetHoldPoint);
            player = null;
        }

    }

    /// <summary>Called at the end of an interaction</summary>
    /// <param name="target">Pass any object data through</param>
    override public void InteractionEndTrigger(object target = null)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Gun.transform.position, -Gun.transform.forward);
    }
}