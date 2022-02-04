using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole_Interactable : Interactable
{
	public LevelHandler levelHandler;
	[SerializeField] bool TryExitLevel;
	public Transform flag;

	Vector3 flagMinPos = new Vector3(-0.187099993f, 0.0520000011f, 0);
	Vector3 flagMaxPos = new Vector3(-0.187099993f, 0.204600006f, 0);
	public Vector3[] flagPositions;
	
	public override void InteractTrigger(object target = null)
	{
		PlayerInteraction player = target as PlayerInteraction;
		
		SetHeld(true, player.GetHoldPoint);
	}
	
	
	public override void InteractionEndTrigger(object target = null)
	{
		if(TryExitLevel)
		{
			levelHandler.ExitLevel();
			WaveController.instance.SetGhostFlag(false);
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
			Debug.Log("wat");
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
		flagPositions = new Vector3[totalPositions];
		float PoleLength = flagMaxPos.y - flagMinPos.y;

		for (int i = 0; i < flagPositions.Length; i++)
		{
			float n = 1f/(totalPositions-1)*(float)i;
			Vector3 pos = Vector3.Lerp(flagMinPos, flagMaxPos, n);
			flagPositions[i] =  pos;
		}
	}

	public void setFlagPos(int progress)
	{
		flag.localPosition = flagPositions[progress];
	}
}