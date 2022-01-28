using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Interactable : Interactable
{
	public GameObject obj;
	
	public TowerInfo towerInfo;
	
	override public void Interact()
	{
		SetHeld(true);
	}
	
	override public void InteractionEnd()
	{
		SetHeld(false);
	}
	
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		
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
				Debug.Log(hit.collider.name);
			}
			else
			{
				obj.SetActive(false);
			}
		}
	}
}