/*
 * Written by:
 * Henrik
*/

using UnityEngine;

// TODO rotate held object

abstract public class Interactable : MonoBehaviour
{
	[SerializeField] KeyCode _interactionButton;
	
	public KeyCode interactionButton => _interactionButton;
	public bool canBeHeld;
	public bool lookTriggerEnabled;
	[HideInInspector] public bool lookIsActive;
	bool lookShouldStay;
	public object lookData;
	[HideInInspector] public bool held;
	Rigidbody rb;
	private Transform originalParent;
	
	public void Start()
	{
		if(!rb)
			rb = GetComponent<Rigidbody>();
		originalParent = transform.parent;
	}
	
	void Update()
	{
		if(lookShouldStay)
			lookIsActive = true;
		else
		{
			lookIsActive = false;
		}
		LookInteraction();
		lookShouldStay = false;
	}

	public bool SetHeld(bool state, Transform parent)
	{
		if(canBeHeld)
		{
			if(rb)
			{
				held = state;
				rb.isKinematic = state;
				if(!held)
					transform.SetParent(originalParent, true);
				else
					transform.SetParent(parent, true);
				rb.velocity = Vector3.zero;
				return held;
			}
			else
				Debug.LogWarning("This object is missing a rigidbody to be picked up " + this);
		}
		return false;
	}
	
	/// <summary>Called at the start of the look interaction</summary>
	/// <param name="target">Pass any object data through</param>
	public void lookTrigger(object target = null)
	{
		lookData = target;
		lookShouldStay = true;
	}
	
	/// <summary>Called when looking at this gameObject</summary>
	virtual public void LookInteraction(){}
	
	/// <summary>Called at the start of an interaction</summary>
	/// <param name="target">Pass any object data through</param>
	abstract public void InteractTrigger(object target = null);
	
	/// <summary>Called at the end of an interaction</summary>
	/// <param name="target">Pass any object data through</param>
	abstract public void InteractionEndTrigger(object target = null);
}