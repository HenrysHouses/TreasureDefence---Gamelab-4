/*
* Written by 
* Henrik
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMachine_ArcadeMachine : ArcadeMachine
{    
    [Header("Claw Machine Specifics")] 
    [SerializeField] PayArcade_Interactable payArcade_Interactable;
    [SerializeField] Transform[] MovementRestriction;
    [SerializeField] Transform ClawBase, Xaxis, Zaxis, Crab, resetPos, grabPos;
    [SerializeField] ClawState state = ClawState.Inactive;
    [SerializeField] float speed = 1;
    [SerializeField] ClawMachine_Grabber grabber;
    bool direction, directionHelper;
    [SerializeField] bool ClawWaiting, hasDropped, waitingReset;



    override public void isPlayingUpdate()
    {
        Vector3 _base = ClawBase.position;
        float diff = Time.deltaTime * speed;
        Vector3 X = Xaxis.position;
        Vector3 Z = Zaxis.position;
        Vector2 TargetPos; 
        Vector2 current;

        switch(state)
        {
            case ClawState.MoveX:
                if(direction)
                {
                    X.x += diff;
                    _base.x += diff;
                }
                else
                {
                    X.x -= diff;
                    _base.x -= diff;
                }

                if(direction && _base.x > MovementRestriction[1].position.x)
                {
                    direction = !direction;
                }
                if(!direction && _base.x < MovementRestriction[0].position.x)
                {
                    direction = !direction;
                }
                Xaxis.position = X;
                ClawBase.position = _base;
                break;

            case ClawState.MoveY:
                if(direction)
                {
                    Z.z += diff;
                    _base.z += diff;
                }
                else
                {
                    Z.z -= diff;
                    _base.z -= diff;
                }

                if(direction && _base.z > MovementRestriction[1].position.z || !direction && _base.z < MovementRestriction[2].position.z)
                {
                    direction = !direction;
                }
                Zaxis.position = Z;
                ClawBase.position = _base;
                break;

            case ClawState.Grab:
                Vector3 Y = Crab.position;
                if(direction)
                    Y.y += diff;
                else
                    Y.y -= diff;

                if(!direction && Crab.position.y < MovementRestriction[3].position.y)
                {
                    direction = !direction;
                    grabber.grab();
                }
                if(direction && Crab.position.y > MovementRestriction[2].position.y)
                {
                    state = ClawState.Reward;
                }
                Crab.position = Y;
                break;

            case ClawState.Reward:

                if(direction)
                {
                    X.x += diff;
                }
                else
                {
                    X.x -= diff;
                }

                if(direction && _base.x > spawnPoint.position.x || !direction && _base.x < spawnPoint.position.x)
                {
                    direction = !direction;
                }

                if(spawnPoint.position.x - Xaxis.position.x > 0.005f ||  Xaxis.position.x - spawnPoint.position.x > 0.005f)
                {
                    Xaxis.position = X;
                    _base.x = X.x;
                }

                if(directionHelper)
                {
                    Z.z += diff;
                }
                else
                {
                    Z.z -= diff;
                }

                if(directionHelper && _base.z > spawnPoint.position.z || !directionHelper && _base.z < spawnPoint.position.z)
                {
                    directionHelper = !directionHelper;
                }

                if(spawnPoint.position.z - Zaxis.position.z < 0.005f || Zaxis.position.z - spawnPoint.position.z < 0.005f)
                {
                    Zaxis.position = Z;
                    _base.z = Z.z;
                }

                TargetPos = new Vector2 (spawnPoint.position.x, spawnPoint.position.z);
                current = new Vector2(ClawBase.position.x, ClawBase.position.z);
                if(Vector2.Distance(TargetPos, current) < 0.05)
                {
                    state = ClawState.Drop;
                    direction = false;
                    directionHelper = false;
                }

                ClawBase.position = _base;
                break;

            case ClawState.Reset:
                if(direction)
                {
                    X.x += diff;
                }
                else
                {
                    X.x -= diff;
                }

                if(direction && _base.x > resetPos.position.x || !direction && _base.x < resetPos.position.x)
                {
                    direction = !direction;
                }

                if(resetPos.position.x - Xaxis.position.x > 0.005f ||  Xaxis.position.x - resetPos.position.x > 0.005f)
                {
                    Xaxis.position = X;
                    _base.x = X.x;
                }

                if(directionHelper)
                {
                    Z.z += diff;
                }
                else
                {
                    Z.z -= diff;
                }

                if(directionHelper && _base.z > resetPos.position.z || !directionHelper && _base.z < resetPos.position.z)
                {
                    directionHelper = !directionHelper;
                }

                if(resetPos.position.z - Zaxis.position.z < 0.005f || Zaxis.position.z - resetPos.position.z < 0.005f)
                {
                    Zaxis.position = Z;
                    _base.z = Z.z;
                }

                TargetPos = new Vector2 (resetPos.position.x, resetPos.position.z);
                current = new Vector2(ClawBase.position.x, ClawBase.position.z);
                if(Vector2.Distance(TargetPos, current) < 0.05)
                {
                    Reset();
                }
                ClawBase.position = _base;
                break;

            case ClawState.Drop:
                if(!ClawWaiting && !hasDropped)
                    StartCoroutine(DropItem());
                if(hasDropped)
                {
                    if(waitingReset)
                    {
                        state = ClawState.Reset;
                        waitingReset = false;
                        ClawWaiting = false;
                        break;
                    }
                    if(!ClawWaiting)
                    {
                        StartCoroutine(wait(2f));
                    }
                }
                break;
        }
    }

    IEnumerator DropItem() 
    {
        ClawWaiting = true;
        yield return new WaitForSeconds(1);
        grabber.drop();
        hasDropped = true;
        ClawWaiting = false;
    }

    IEnumerator wait(float sec)
    {
        ClawWaiting = true;
        yield return new WaitForSeconds(sec);
        waitingReset = true;
    }

	override public bool WinCondition()
    {
        return false;
    }

	override public bool LoseCondition()
    {
        return false;
    }

	override public void LoseTrigger()
    {

    }

	override public void Reward()
    {

    }

	override public void Reset()
    {
        state = ClawState.Inactive;
        isPlaying = false;
        ClawWaiting = false;
        hasDropped = false;
        waitingReset = false;
    }

	override public void StartSetup()
    {
        state = ClawState.MoveX;
    }


    public void ButtonPush()
    {
        switch(state)
        {
            case ClawState.Inactive:
                payArcade_Interactable.Pay();
                break;
            
            case ClawState.MoveX:
                state = ClawState.MoveY;
                break;
            
            case ClawState.MoveY:
                state = ClawState.Grab;
                direction = false;
                break;
        }
    }

    [System.Serializable]
    enum ClawState
    {
        Inactive,
        MoveX,
        MoveY,
        Grab,
        Reward,
        Drop,
        Reset
    }
}
