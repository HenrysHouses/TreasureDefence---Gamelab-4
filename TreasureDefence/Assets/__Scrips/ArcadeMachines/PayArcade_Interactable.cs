/*
 * Written by:
 * Henrik
*/

using UnityEngine;

public class PayArcade_Interactable : TD_Interactable
{
    [SerializeField] WackAMoleController arcade;

    public override void InteractTrigger(object target = null)
    {
        arcade.StartGame();
    }
}
