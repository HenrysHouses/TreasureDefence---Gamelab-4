using System;
using System.Collections;
using UnityEngine;

public class RaycastForInteractable : MonoBehaviour
{
	// public string tag;

	[SerializeField]
	public LayerMask layerMask;

	public GameObject vendorGreeting;
	public GameObject vendorMenu;
	public ShopManager shopManager;

	public GameObject ePrompt;
	
	private GameObject item;
	private bool hasItem;
	private TD_Interactable heldItem;

	public Transform objectHolderPosition;
	public Transform cam;

	private float holdOffset;

	private void Start()
	{
		objectHolderPosition.transform.position = cam.transform.position + cam.transform.forward * 1.5f;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.O))
		{
			CurrencyManager.instance.AddMoney(10);
		}

		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		//Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.magenta);

		if (Physics.Raycast(ray, out hit, 2.7f, layerMask))
		{
			switch (hit.transform.tag)
			{
				case "Cup":
					StartNextWave();
					break;

				case "Vendor":
					Vendor();
					break;
			}
		}
		else
		{
			Clear();
		}

		if (Input.GetKeyDown(KeyCode.Mouse0) && !hasItem)
		{
			var pickupRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			// RaycastHit pickupHit;

			if (Physics.Raycast(ray, out hit, 3.5f))
			{
				if (!hasItem)
				{
					PickUp(hit.collider.gameObject);
				}
			}

		}
		// ! depreciated
		if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("this method has been depreciated and functionality may not work " + this);
			hasItem = false;
			if (item != null)
			{
				item.GetComponent<Rigidbody>().useGravity = true;
				// heldItem.SetHeld(false);
			}
			item = null;
		}

		if (hasItem)
		{
			if(Input.mouseScrollDelta.x !=0)
			{
				holdOffset -= Input.mouseScrollDelta.x * 50 * Time.deltaTime;
				holdOffset = Mathf.Clamp(holdOffset, 1f, 30f);
				
				objectHolderPosition.transform.position = cam.transform.position + cam.transform.forward * holdOffset;
			}
			else if (Input.mouseScrollDelta.y != 0)
			{
				holdOffset += Input.mouseScrollDelta.y * 50 * Time.deltaTime;
				holdOffset = Mathf.Clamp(holdOffset, 1f, 30f);
				
				objectHolderPosition.transform.position = cam.transform.position + cam.transform.forward * holdOffset;
			}
		
			Debug.Log("Offset: " + holdOffset);

			item.transform.position = objectHolderPosition.transform.position;
		}
		
		if (Input.GetKeyDown(KeyCode.Space))
			StartNextWave();
	}

	// ! depreciated
	void PickUp(GameObject gameobject)
	{
		Debug.Log("this method has been depreciated and functionality may not work " + this);
		heldItem = gameobject.GetComponent<TD_Interactable>();

		if (heldItem != null)
		{
			// heldItem.SetHeld(true);
			
			item = gameobject;
			item.GetComponent<Rigidbody>().useGravity = false;
			hasItem = true;
		}
	}
	
	void Clear()
	{
		vendorGreeting.SetActive(false);
		vendorMenu.SetActive(false);
		ePrompt.SetActive(false);
	}

	// ? replacement for cup()
	// ! depreciated, Check waveTrigger_Interactable.cs
	
	void StartNextWave()
	{
		if(!GameManager.instance.GetWaveController().waveIsPlaying)
		{
			ePrompt.SetActive(true);
			
			if (Input.GetKeyDown(KeyCode.E))
			{
				Debug.LogWarning("Wave Started");

				GameManager.instance.GetWaveController().nextWave();
			}
		}
		else
		{
			ePrompt.SetActive(false);
		}
		
	}

	// ! depreciated
	/*
	void Cup()
	{
		if (!GameManager.instance.isWave)
		{
			ePrompt.SetActive(true);
			
			if (Input.GetKeyDown(KeyCode.E))
			{
				Debug.LogWarning("Shot ray.");

				GameManager.instance.StartWave();
			}
		}
		else
		{
			ePrompt.SetActive(false);
		}
	}
	*/
	
	void Vendor()
	{
		vendorGreeting.SetActive(true);

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			vendorMenu.SetActive(true);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			if (vendorMenu.activeInHierarchy)
			{
				// GameObject.FindGameObjectWithTag("Vendor").GetComponent<ShopManager>().SpawnTowerAtPoint();
				RotateTowardsTransform.instance.DoBuyAnimation();
			}
				//shopManager.PayForTower();
		}
	}

	void Tower()
	{
		// Example
		// TowerInfoUI.SetActive(true);
	}

	void Enemy()
	{
		// Example
		// EnemyInfoUI.SetActive(true);
	}

	void Untagged()
	{
	}

	void RayPick()
	{
	}

}