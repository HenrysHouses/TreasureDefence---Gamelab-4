/*
 * Written by:
 * Henrik
*/

using UnityEngine;

// TODO scroll to move hold point
// TODO move hold point backwards when facing walls/obstructions

public class PlayerInteraction : MonoBehaviour
{
	[SerializeField] float InteractionDist = 3.5f;
	[SerializeField] Transform _holdPoint;
	public Transform GetHoldPoint => _holdPoint;
	public Interactable currentInteractable;
	private bool isBusy;
	
	private void Update()
	{
		if (!isBusy) // Start interaction
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, InteractionDist))
			{
				if(hit.collider.TryGetComponent<Interactable>(out currentInteractable))
				{
					if(currentInteractable.lookTriggerEnabled)
						currentInteractable.lookTrigger(this);
					
					if(Input.GetKeyDown(currentInteractable.interactionButton))
						startInteraction();
				}
			}
		}
		else if(currentInteractable) // End interaction OR wait until interaction is done
		{
			// * This code has been moved and changed.
			// if(currentInteractable.canBeHeld && currentInteractable.held) // ? dont know if this code should be here or on the interactable class
			// {
			// 	currentInteractable.transform.position = holdPoint.position;
			// }

			if(Input.GetKeyUp(currentInteractable.interactionButton))
			{
				endInteraction();
			}
		}
	}
	
	private void startInteraction()
	{
		currentInteractable.InteractTrigger(this);
		isBusy = true;
	}
	
	private void endInteraction()
	{
		currentInteractable.InteractionEndTrigger(this);
		currentInteractable = null;
		isBusy = false;
	}
}