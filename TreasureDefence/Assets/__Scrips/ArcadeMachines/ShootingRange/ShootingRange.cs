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
    [Tooltip("The Speed of the Target")]
    public int TargetMovementSpeed;
    public float num;

    private GameObject TargetPos1, TargetPos2;
    // Base Arcade Behaviour

    //Start the Game. What it costs.
    public override void StartSetup()
    {  
        base.StartSetup();
        
        Debug.Log("This is what happens at start");
    }

    public void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.tag == "Player")
        {
            StartGame();
            Debug.Log("Works?");
            TargetMovement = true;
        }
    }

    public override void isPlayingUpdate()
    {
        num += TargetMovementSpeed * Time.deltaTime;
        Vector3 pos = Target.transform.position;
        pos.z = PingPongExtention(num, 0, 1) + -5;
        Target.transform.position = pos;

            Gun.transform.position = GunHoldPosition.transform.position;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {

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
    
    public float PingPongExtention(float t, float rangeFrom, float rangeTo)
    {
        float remapped = 0;
        if (t % rangeTo > rangeTo/2)
        {
            remapped = ExtensionMethods.Remap(t % rangeTo, rangeFrom, rangeTo, rangeTo, rangeFrom);
        }
        else
        {
            remapped = t % rangeTo;
        }
            return remapped;

    }


    //While Playing.

}
