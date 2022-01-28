using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Interactable : Interactable
{
	
	override public void Interact()
	{
		SetHeld(true);
	}
	
	override public void InteractionEnd()
	{
		SetHeld(false);
	}
	
	// Update is called once per frame
	void Update()
	{
		// something should happen here
	}
}
