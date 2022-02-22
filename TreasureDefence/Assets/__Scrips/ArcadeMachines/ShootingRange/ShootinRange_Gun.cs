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
    public MeshRenderer meshRenderer;

    /// <summary>Called at the start of an interaction</summary>
    /// <param name="target">Pass any object data through</param>
    override public void InteractionStartTrigger(object target = null)
    {
        player = target as PlayerInteraction;

        SetHeld(true, player.GetHoldPoint);
    }

	override public void VRInteractionStartTrigger()
    {

    }

    private new void Update()
    {
        base.Update();

        
        //if the target is hit 5 times within ? many seconds.
        timer -= Time.deltaTime;

        ShootGun();

        if (Input.GetMouseButtonDown(1))
        {
            SetHeld(false, player.GetHoldPoint);
            player = null;
        }

    }

    
    public void ShootGun()
    {

        if(Input.GetMouseButtonDown(0) && timer <= 0)
        {
            timer = ReloadTime;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(Gun.transform.position, Gun.transform.forward);

            if (Physics.Raycast(Gun.transform.position, -Gun.transform.forward, out hit))
            {
                
                if (hit.transform.CompareTag("Target"))
                {
                    shootingrange.HitTarget++;
                    Debug.Log("hit = " + shootingrange.HitTarget);
                    meshRenderer.material.color = Color.red;             //Fix this later. -Mikkel.
                    Invoke("ResetColor", 0.15f);
                }
            }
        }
    }

    private void ResetColor()
    {
        meshRenderer.material.color = Color.white;         

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Gun.transform.position, -Gun.transform.forward);
    }
}