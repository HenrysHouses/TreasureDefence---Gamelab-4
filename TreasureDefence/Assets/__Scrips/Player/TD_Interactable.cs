/*
 * Written by:
 * Henrik
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// TODO rotate held object

abstract public class TD_Interactable : MonoBehaviour
{
	[SerializeField] KeyCode _interactionButton;
	public KeyCode interactionButton => _interactionButton;

	public bool canBeHeld;
	[SerializeField] bool canBeMoved;
	public bool lookTriggerEnabled;
	[HideInInspector] public bool lookIsActive;
	bool lookShouldStay;
	public object lookData;
	[HideInInspector] public bool held;
	[HideInInspector] public Rigidbody rb;
	private Transform originalParent;
	[SerializeField] Renderer[] _renderer;
	bool highlightOffOverride;
	[SerializeField] static Shader highlight;

	Collider _thisCollider;
	public Collider getCollider()
	{
		if(_thisCollider)
			return _thisCollider;
		_thisCollider = GetComponent<Collider>();
		return _thisCollider;
	}
	
	public void Start()
	{
		if(!rb)
			rb = GetComponent<Rigidbody>();
		originalParent = transform.parent;
	}
	
	public void Update()
	{
		if(lookShouldStay)
			lookIsActive = true;
		else
		{
			lookIsActive = false;
		}
		LookInteraction();
		lookShouldStay = false;

		if(Input.GetKeyDown(KeyCode.L))
			toggleHighlight();
	}

	public void toggleHighlight()
	{
		if(!highlightOffOverride)
		{
			foreach (var rend in _renderer)
			{
				foreach (var mat in rend.materials)
				{
					if(mat.shader == highlight)
					{
						float state = mat.GetFloat("_FernelToggle");
						mat.SetFloat("_FernelToggle", 1-state);
					}
				}
			}
		}
	}

	public void setHighlightOffOverride(bool state)
	{	 
		if(state)
			SetHighlight(false);
		highlightOffOverride = state;
	}

	public void SetHighlight(bool state)
	{
		if(!highlightOffOverride)
		{
			foreach (var rend in _renderer)
			{
				foreach (var mat in rend.materials)
				{
					if(mat.shader == highlight)
					{
						if(state)
							mat.SetFloat("_FernelToggle", 1);
						else
							mat.SetFloat("_FernelToggle", 0);
					}
				}
			}
		}
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
	abstract public void InteractionStartTrigger(object target = null);
	
	/// <summary>Called at the end of an interaction</summary>
	/// <param name="target">Pass any object data through</param>
	virtual public void InteractionEndTrigger(object target = null){}

	virtual public void VRLookInteraction(){}
	abstract public void VRInteractionStartTrigger();
	virtual public void VRInteractionEndTrigger(){}
}

