using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMachine_ArcadeMachine : ArcadeMachine
{    
    [Header("Claw Machine Specifics")] 
    [SerializeField] PayArcade_Interactable payArcade_Interactable;
    [SerializeField] Transform[] MovementRestriction;


    override public void isPlayingUpdate()
    {

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

    }


    public void ButtonPush()
    {

    }
}
