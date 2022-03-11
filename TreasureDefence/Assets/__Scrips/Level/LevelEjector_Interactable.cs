using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEjector_Interactable : TD_Interactable
{
    [SerializeField] LevelHandler levelHandler;

    public override void InteractionStartTrigger(object target = null)
    {
        levelHandler.ExitLevel();
    }

    public override void VRInteractionStartTrigger()
    {
        levelHandler.ExitLevel();
    }
}