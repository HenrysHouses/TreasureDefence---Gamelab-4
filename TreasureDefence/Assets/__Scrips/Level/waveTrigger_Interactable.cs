using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveTrigger_Interactable : TD_Interactable
{
	public GameObject ePrompt;
	[SerializeField] LevelHandler levelHandler;
	
	public override void InteractionStartTrigger(object target = null)
	{
		if(GameManager.instance.GetWaveController() != null && levelHandler.LevelIsReady)
			if(!GameManager.instance.GetWaveController().waveIsPlaying)
				GameManager.instance.GetWaveController().nextWave();	
	}
	
	override public void VRInteractionStartTrigger()
	{
		Debug.Log("VR trigger");
		if(GameManager.instance.GetWaveController() != null && levelHandler.LevelIsReady)
			if(!GameManager.instance.GetWaveController().waveIsPlaying)
				GameManager.instance.GetWaveController().nextWave();
	}
	
	
	override public void LookInteraction()
	{
		if(levelHandler.LevelIsReady)
			ePrompt.SetActive(lookIsActive);
		// else
		// 	Debug.Log("Something should happen here?");
	}

	override public void VRLookInteraction()
	{
		Debug.Log("VR look");
		if(levelHandler.LevelIsReady)
			ePrompt.SetActive(lookIsActive);
	}
	
}
