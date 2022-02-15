using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRange : ArcadeMachine
{   //Mikkel tried to make this.
    public GameObject Target;
    public GameObject Gun; //When the target pics up the gun, the game starts.
    [Tooltip("The position of the gun, when picked up.")]
    public GameObject GunHoldPosition;

    [Tooltip("A bool to activate the targets left and right movement.")]
    public bool TargetMovement;

    private GameObject TargetPos1, TargetPos2;
    // Base Arcade Behaviour

    //Start the Game. What it costs.
    public override void StartSetup()
    {  
        TargetMovement = true;

        base.StartSetup();
        
        Gun.transform.position = GunHoldPosition.transform.position;
        Debug.Log("This is what happens at start");
    }

    public void OnTriggerEnter(Collider other)
    {   
            StartSetup();
    }

    public override void isPlayingUpdate()
    {
        if (TargetMovement = true)
        {
            Target.transform.position = TargetPos1.transform.position;

          /*  if()
            {
                Target.transform.position = TargetPos2.transform.position;
            }     */
        }

        

        return ;
    }

    
    public override bool WinCondition()
    {
        //if the target is hit 5 times within ? many seconds.
        return false;
    }
    
    public override bool LooseCondition()
    {
        return false;
    }
    public override void Reward()
    {
        Instantiate(towerRewardPrefab, spawnPoint.position, Quaternion.identity);
    }
    public override void Reset()
    {

    }
    


    //While Playing.

}
