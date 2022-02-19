/*
 * Written by:
 * Henrik
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Interactable : TD_Interactable
{
	public LevelWaveSequence LevelData;
	public GameObject levelHighlight, waterHighlight;
	override public void InteractTrigger(object target = null)
	{
		PlayerInteraction player = target as PlayerInteraction;
		
		SetHeld(true, player.GetHoldPoint);
	}
	
	override public void InteractionEndTrigger(object target = null)
	{
		PlayerInteraction player = target as PlayerInteraction;
		
		SetHeld(false, player.GetHoldPoint);
	}	
}
