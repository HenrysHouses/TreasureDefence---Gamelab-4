using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRange : ArcadeMachine
{   //Mikkel tried to make this.
    public GameObject Target;

    [Tooltip("A bool to activate the targets left and right movement.")]
    public bool TargetMovement;
    [Tooltip("The Speed of the Target")]
    public int TargetMovementSpeed, HitTarget;
    public float num, StartTimer;
    float timeLeft;
  


    // Base Arcade Behaviour

    //Start the Game. What it costs.
    public override void StartSetup()
    {
        base.StartSetup();
        HitTarget = 0;
        timeLeft = StartTimer;

    }

    public override void isPlayingUpdate()
    {
        num += TargetMovementSpeed * Time.deltaTime;
        Vector3 pos = Target.transform.position;
        pos.z = PingPongExtention(num, 0, 1) + -5;
        Target.transform.position = pos;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            Debug.Log("Time ran out.");
            LooseCondition();
        }

        return;
    }


    public override bool WinCondition()
    {

        if (HitTarget >= 5)
        {
            return true;
        }
        return false;
    }

    public override bool LooseCondition()
    {
        if (timeLeft <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void Reward()
    {
        Instantiate(towerRewardPrefab, spawnPoint.position, Quaternion.identity);
        HitTarget = 0;
    }
    public override void Reset()
    {

    }

    public float PingPongExtention(float t, float rangeFrom, float rangeTo)
    {
        float remapped = 0;
        if (t % rangeTo > rangeTo / 2)
        {
            remapped = ExtensionMethods.Remap(t % rangeTo, rangeFrom, rangeTo, rangeTo, rangeFrom);
        }
        else
        {
            remapped = t % rangeTo;
        }
        return remapped;

    }


    //While Playing.  Damit Rune.

}
