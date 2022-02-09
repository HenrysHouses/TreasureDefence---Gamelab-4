/*
 * Written by:
 * Henrik
*/

using UnityEngine;

// TODO scroll to move hold point
// TODO move hold point backwards when facing walls/obstructions

public class PlayerInteraction : MonoBehaviour
{
	
	
	
	[SerializeField] float InteractionDist = 5f, holdOffset;
	[SerializeField] Transform _holdPoint, objectHolderPosition, cam;
	public Transform GetHoldPoint => _holdPoint;
	public Interactable currentInteractable;
	private bool isBusy;
	private GameObject item;
	public bool hasItem;


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
                    {
						startInteraction();
						item = hit.collider.gameObject;
                    }
				}

			}
			/*if (hasItem)
			{
				if (Input.mouseScrollDelta.x != 0)
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
			}		   */
		}
		else if(currentInteractable) // End interaction OR wait until interaction is done
		{
			if(Input.GetKeyUp(currentInteractable.interactionButton))
			{
				endInteraction();
			}	
		}		


	}
	


	private void startInteraction()
	{
		hasItem = true;
		currentInteractable.InteractTrigger(this);
		isBusy = true;
	}
	
	private void endInteraction()
	{
		hasItem = false;
		currentInteractable.InteractionEndTrigger(this);
		currentInteractable = null;
		isBusy = false;
	}
}