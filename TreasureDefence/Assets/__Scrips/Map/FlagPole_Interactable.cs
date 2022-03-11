using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class FlagPole_Interactable : TD_Interactable
{
	[SerializeField] LevelHandler levelHandler;
	bool TryExitLevel;
	public Transform flag;

	Vector3 flagMinPos = Vector3.zero;
	Vector3 flagMaxPos = new Vector3(0, 0.2408f, 0);
	Vector3[] flagPositions;
	
	new void Start()
	{
		base.Start();
		findDependencies();
	}

	void findDependencies()
	{
		try
		{
			levelHandler = GameObject.FindObjectOfType<LevelHandler>();	
		}
		catch
		{
			if(!levelHandler)
				Debug.LogError("LevelHandler is missing in the scene");
		}
	}

	public override void InteractionStartTrigger(object target = null)
	{
		PlayerInteraction player = target as PlayerInteraction;
		
		SetHeld(true, player.GetHoldPoint);

	}
	
	override public void VRInteractionStartTrigger()
	{
	}

	override public void VRInteractionEndTrigger()
	{
		if(!levelHandler)
			findDependencies();
		if(TryExitLevel)
		{
			levelHandler.ExitLevel();
			TryExitLevel = false;
		}

		Vector3 rot = transform.eulerAngles;
		Vector3 resetRot = new Vector3(0, rot.y, 0);
		transform.eulerAngles = resetRot;
	}
	
	public override void InteractionEndTrigger(object target = null)
	{
		if(!levelHandler)
			findDependencies();
		if(TryExitLevel)
		{
			levelHandler.ExitLevel();
			TryExitLevel = false;
		}

		PlayerInteraction player = target as PlayerInteraction;
		
		SetHeld(false, player.GetHoldPoint);
		Vector3 rot = transform.eulerAngles;
		Vector3 resetRot = new Vector3(0, rot.y, 0);
		transform.eulerAngles = resetRot;
	}
	
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("GhostFlag/Snap"))
		{
			TryExitLevel = true;
			transform.position = other.transform.position;
			Vector3 rot = transform.eulerAngles;
			Vector3 resetRot = new Vector3(0, rot.y, 0);
			transform.eulerAngles = resetRot;
		}
	}
	
	/// <summary>
	/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("GhostFlag/Snap"))
		{
			TryExitLevel = false;
		}
	}

	public void calculateFlagPositions(int totalPositions)
	{
		flagPositions = new Vector3[totalPositions+1];
		float PoleLength = flagMaxPos.y - flagMinPos.y;

		for (int i = 0; i < flagPositions.Length; i++)
		{
			float n = 1f/(totalPositions)*(float)i;
			Vector3 pos = Vector3.Lerp(flagMinPos, flagMaxPos, n);
			flagPositions[i] =  pos;
		}
	}

	public void setFlagPos(int progress)
	{
		flag.localPosition = flagPositions[progress];
	}
}