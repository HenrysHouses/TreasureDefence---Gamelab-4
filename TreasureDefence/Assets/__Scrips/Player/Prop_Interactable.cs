/*
 * Written by:
 * Henrik
*/
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class Prop_Interactable : TD_Interactable
{
	override public void InteractionStartTrigger(object target = null)
	{
		PlayerInteraction player = target as PlayerInteraction;
		
		SetHeld(true, player.GetHoldPoint);
	}
	
	override public void InteractionEndTrigger(object target = null)
	{
		PlayerInteraction player = target as PlayerInteraction;
		
		SetHeld(false, player.GetHoldPoint);
	}

	override public void VRInteractionStartTrigger(){}
}
