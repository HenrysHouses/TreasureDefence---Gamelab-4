using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveTrigger_Interactable : Interactable
{
	public GameObject ePrompt;
	
	public override void InteractTrigger(object target = null)
	{
		if(GameManager.instance.GetWaveController() != null)
			if(!GameManager.instance.GetWaveController().waveIsPlaying)
				GameManager.instance.GetWaveController().nextWave();	
	}
	
	public override void InteractionEndTrigger(object target = null){}
	
	override public void LookInteraction()
	{
		ePrompt.SetActive(lookIsActive);
	}
}
