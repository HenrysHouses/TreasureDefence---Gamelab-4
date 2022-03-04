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
	TowerBehaviour towerBehaviour;
	public TowerInfo towerInfo;
	Color Color1, Color2;
	bool invalidPlacement;
	[SerializeField] MeshRenderer RangeHighlight;

	new void Start()
	{
		base.Start();
		Color1 = RangeHighlight.material.GetColor("_Color");
		Color2 = RangeHighlight.material.GetColor("_Color2");
		towerBehaviour = GetComponent<TowerBehaviour>();
	}
	
	override public void InteractionStartTrigger(object target = null)
	{
		PlayerInteraction player = target as PlayerInteraction;
		
		SetHeld(true, player.GetHoldPoint);
		obj.SetActive(true);
		towerBehaviour.canShoot = false;
	}
	
	override public void InteractionEndTrigger(object target = null)
	{
		PlayerInteraction player = target as PlayerInteraction;
		
		SetHeld(false, player.GetHoldPoint);
		obj.SetActive(false);
		if(!invalidPlacement)
			towerBehaviour.canShoot = true;
	}
	
	override public void VRInteractionStartTrigger()
	{
		held = true;
		towerBehaviour.canShoot = false;
		obj.SetActive(true);
	}

	override public void VRInteractionEndTrigger()
	{
		held = false;
		if(!invalidPlacement)
			towerBehaviour.canShoot = true;
		obj.SetActive(false);
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
				if(!hit.collider.CompareTag("ValidPlacement"))
				{
					RangeHighlight.material.SetColor("_Color", Color.red);
					RangeHighlight.material.SetColor("_Color2", Color.red);
					invalidPlacement = true;
				}
				else
				{
					RangeHighlight.material.SetColor("_Color", Color1);
					RangeHighlight.material.SetColor("_Color2", Color2);
					invalidPlacement = false;
				}


				if(obj)
				{
					if (!obj.activeSelf)
						obj.SetActive(true);	
				}

				obj.transform.position = hit.point;
				obj.transform.rotation = Quaternion.identity;
			}
			else
			{
				obj.SetActive(false);
			}
		}
	}
}