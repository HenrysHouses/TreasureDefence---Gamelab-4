/*
 * Written by:
 * Henrik
*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class Tower_Interactable : TD_Interactable
{
	public GameObject obj;
	
	public TowerInfo towerInfo;
	
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
	
	override public void VRInteractionStartTrigger()
	{
		held = true;
	}

	override public void VRInteractionEndTrigger()
	{
		held = false;
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	new void Update()
	{
		base.Update();
		if(held)
		{
			Vector3 rot = transform.eulerAngles;
			rot = new Vector3(0, rot.y, 0);
			transform.eulerAngles = rot;
		}
	}
	
	private void FixedUpdate()
	{
		if (held)
		{
			RaycastHit hit;

			Debug.DrawRay(transform.position + (Vector3.down * 0.1f), Vector3.down, Color.red);
			
			if (Physics.Raycast(transform.position + (Vector3.down * 0.1f), Vector3.down, out hit, 2f))
			{				
				if(obj)
				{
					if (!obj.activeSelf)
						obj.SetActive(true);	
				}

				obj.transform.position = hit.point;
			}
			else
			{
				obj.SetActive(false);
			}
		}
	}
}