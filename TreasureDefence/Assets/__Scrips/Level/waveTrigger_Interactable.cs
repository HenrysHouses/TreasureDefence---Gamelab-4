using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveTrigger_Interactable : TD_Interactable
{
	[SerializeField] LevelHandler levelHandler;
	
	new void Start()
	{
		base.Start();
		levelHandler = GameObject.FindObjectOfType<LevelHandler>();	
	}

	public override void InteractionStartTrigger(object target = null)
	{
		if(GameManager.instance.GetWaveController() != null && levelHandler.LevelIsReady)
			if(!GameManager.instance.GetWaveController().waveIsPlaying)
				GameManager.instance.GetWaveController().nextWave();	
	}
	
	override public void VRInteractionStartTrigger()
	{
		if(GameManager.instance.GetWaveController() != null && levelHandler.LevelIsReady)
			if(!GameManager.instance.GetWaveController().waveIsPlaying)
				GameManager.instance.GetWaveController().nextWave();
	}
	
	
	override public void LookInteraction()
	{
		// if(levelHandler.LevelIsReady)
			// ePrompt.SetActive(lookIsActive);
		// else
		// 	Debug.Log("Something should happen here?");
	}

	override public void VRLookInteraction()
	{
		Debug.Log("VR look");
		// if(levelHandler.LevelIsReady)
			// ePrompt.SetActive(lookIsActive);
	}
	
}
