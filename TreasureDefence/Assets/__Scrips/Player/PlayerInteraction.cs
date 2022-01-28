using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{
	[SerializeField] KeyCode InteractionButton;
	[SerializeField] float InteractionDist = 3.5f;
	[SerializeField] Transform holdPoint;
	public Interactable currentInteractable;
	
	private UnityEvent OnInteractionEnd = new UnityEvent();
	
	
	void Start()
	{
		
	}
	

	private void Update()
	{
		if (!currentInteractable) // Start interaction
		{
			if (Input.GetKeyDown(InteractionButton))
			{
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				
				if (Physics.Raycast(ray, out hit, InteractionDist))
				{
					if(hit.collider.TryGetComponent<Interactable>(out currentInteractable))
					{
						currentInteractable.Interact();
					}
				}
			}
		}
		else // End interaction OR wait until interaction is done
		{
			if(currentInteractable.canBeHeld)
			{
				currentInteractable.transform.position = holdPoint.position;
			}

			if(Input.GetKeyUp(InteractionButton))
			{
				endInteraction();
			}
		}
	}
	
	public void endInteraction()
	{
		currentInteractable.InteractionEnd();
		currentInteractable = null;
	}
}