using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class waveTrigger_Interactable : TD_Interactable
{
	[SerializeField] LevelHandler levelHandler;
	[SerializeField] StudioEventEmitter _AudioSource;
	
	new void Start()
	{
		base.Start();
		levelHandler = GameObject.FindObjectOfType<LevelHandler>();	
	}

	public override void InteractionStartTrigger(object target = null)
	{
		tryStartWave();	
	}
	
	override public void VRInteractionStartTrigger()
	{
		tryStartWave();
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.GetComponent<VFX_BulletController>())
		{
			tryStartWave();
		}

		if(collision.collider.CompareTag("Mallet"))
		{
			if(collision.transform.GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
				tryStartWave();
		}
	}	

	public void tryStartWave()
	{
		_AudioSource.Play();
		if(GameManager.instance.GetWaveController() != null && levelHandler.LevelIsReady)
			if(!GameManager.instance.GetWaveController().waveIsPlaying)
				GameManager.instance.GetWaveController().nextWave();
	}
}
