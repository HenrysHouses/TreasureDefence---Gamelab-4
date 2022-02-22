/*
 * Written by:
 * Henrik
*/

using UnityEngine;

public class PayArcade_Interactable : TD_Interactable
{
    [SerializeField] ArcadeMachine arcade;
   

    public override void InteractionStartTrigger(object target = null)
    {
        arcade.StartGame();
    }

    override public void VRInteractionStartTrigger()
    {
        arcade.StartGame();
       
    }
}
