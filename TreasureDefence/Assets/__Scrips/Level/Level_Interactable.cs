/*
 * Written by:
 * Henrik
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class Level_Interactable : TD_Interactable
{
	public LevelWaveSequence LevelData;

	new void Start()
	{
		base.Start();
		
	}
	override public void InteractionStartTrigger(object target = null)
	{
		PlayerInteraction player = target as PlayerInteraction;
		
		SetHeld(true, player.GetHoldPoint);

		if(GameManager.instance)
			GameManager.instance.LevelPickupHighlight.enabled = false;
	}
	
	override public void InteractionEndTrigger(object target = null)
	{
		PlayerInteraction player = target as PlayerInteraction;
		
		SetHeld(false, player.GetHoldPoint);
	}	

	override public void VRInteractionStartTrigger()
	{
		if(GameManager.instance)
		{
			GameManager.instance.LevelPickupHighlight.enabled = false;
			GameManager.instance.WaterHighlight.enabled = true;
		}
	}
}
