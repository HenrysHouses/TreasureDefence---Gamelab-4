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
	[SerializeField] Transform detectionRaycastPos;
	[SerializeField] GameObject InvalidPlacementMarker;
	public GameObject obj;
	TowerBehaviour towerBehaviour;
	Color Color1, Color2;
	bool invalidPlacement;
	//River mode is just going to comparetag with River and if it finds river its gonna set it as valid placement.
	public bool RiverMode;
	[SerializeField] MeshRenderer RangeHighlight;

	new void Start()
	{
		base.Start();
		Color1 = RangeHighlight.material.GetColor("_Color");
		Color2 = RangeHighlight.material.GetColor("_Color2");
		towerBehaviour = GetComponent<TowerBehaviour>();
		rb = GetComponent<Rigidbody>();
		InvalidPlacementMarker.SetActive(false);
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
			rb.constraints = RigidbodyConstraints.FreezeRotation;
			
			if(InvalidPlacementMarker.activeSelf)
				InvalidPlacementMarker.SetActive(false);
		}

		if(RiverMode == false)
        {
			
		}
		
	}
	
	private void FixedUpdate()
	{
		Vector3 scale = new Vector3(towerBehaviour.towerRange * 2, 0.5f, towerBehaviour.towerRange * 2);
		// scale = new Vector3(scale.x, );
		ExtensionMethods.SetGlobalScale(RangeHighlight.transform, scale);

		if (held)
		{
			RaycastHit hit;

			Debug.DrawRay(detectionRaycastPos.position, Vector3.down, Color.red);
			
			if (Physics.Raycast(detectionRaycastPos.position, Vector3.down, out hit, 2f))
			{
				if (!RiverMode)
                {

					if (!hit.collider.CompareTag("ValidPlacement"))
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

				}
				else
                {
					 if (!hit.collider.CompareTag("River"))
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

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("River") && !invalidPlacement && RiverMode)
		{
			rb.constraints = RigidbodyConstraints.FreezeAll;
		}
		
		if(other.CompareTag("River") && !RiverMode)
		{
			InvalidPlacementMarker.SetActive(true);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.CompareTag("ValidPlacement") && !invalidPlacement && !RiverMode)
		{
			rb.constraints = RigidbodyConstraints.FreezeAll;
		}

		if(collision.collider.CompareTag("ValidPlacement") && RiverMode)
		{
			InvalidPlacementMarker.SetActive(true);
		}
	}
}