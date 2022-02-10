/*
 * Written by:
 * Henrik
*/

public class Prop_Interactable : Interactable
{
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
