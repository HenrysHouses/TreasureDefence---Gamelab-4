using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayArcade_Interactable : Interactable
{
    [SerializeField] WackAMoleController arcade;

    public override void InteractTrigger(object target = null)
    {
        if(CurrencyManager.instance.money > arcade.ArcadeCost && !arcade.isPlaying)
        {
            CurrencyManager.instance.SubtractMoney(arcade.ArcadeCost);
            arcade.StartGame();
        }
    }

    public override void InteractionEndTrigger(object target = null)
    {

    }
}
