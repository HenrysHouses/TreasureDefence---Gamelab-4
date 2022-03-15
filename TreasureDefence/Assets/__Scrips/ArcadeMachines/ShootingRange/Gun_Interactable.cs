using UnityEngine;
using FMODUnity;

public class Gun_Interactable : TD_Interactable
{   //Mikkel & Henrik, (Mostly HenriK).

    StudioEventEmitter _Audiosource;
    [SerializeField] GameObject bulletPrefab;
    public Transform barrelTransform;
    [SerializeField]
    ShootingRangeController_ArcadeMachine shootingrange;
    PlayerInteraction player;
    float timer;
    public float ReloadTime;
    public MeshRenderer meshRenderer;

    new void Start()
    {
        base.Start();
        _Audiosource = GetComponent<StudioEventEmitter>();
    }

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

        if(player)
        {
            if(Input.GetMouseButtonDown(0))
                ShootGun();

            if (Input.GetMouseButtonDown(1))
            {
                SetHeld(false, player.GetHoldPoint);
                player = null;
            }
        }

    }

    
    public void ShootGun()
    {

        if(timer <= 0)
        {
            timer = ReloadTime;

            Debug.DrawRay(transform.position, -transform.forward);

            _Audiosource.Play();
            Instantiate(bulletPrefab, barrelTransform.position, barrelTransform.rotation);

            // RaycastHit hit;
            // if (Physics.Raycast(barrelTransform.position, barrelTransform.forward, out hit))
            // {
                
            //     if (hit.transform.CompareTag("Target"))
            //     {
            //         shootingrange.HitTarget++;
            //         // Debug.Log("hit = " + shootingrange.HitTarget);
            //         meshRenderer.material.color = Color.red;             //Fix this later. -Mikkel.
            //         Invoke("ResetColor", 0.15f);
            //     }
            // }
        }
    }

    private void ResetColor()
    {
        meshRenderer.material.color = Color.white;         
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(barrelTransform.position, barrelTransform.forward);
    }
}