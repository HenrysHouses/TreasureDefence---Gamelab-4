using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
abstract public class Interactable : MonoBehaviour
{
	public bool canBeHeld;
	[HideInInspector] public bool held;
	private Rigidbody rb;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public bool SetHeld(bool hold)
	{
		if(canBeHeld)
		{
			held = hold;
			rb.useGravity = !hold;
			return held;
		}
		return false;
	}
	
	abstract public void Interact();
	abstract public void InteractionEnd();
}