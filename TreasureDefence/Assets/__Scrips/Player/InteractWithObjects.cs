using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{    //Mikkel stole Runes code. I take credit.
    public bool hasItem;
    private GameObject item;
    public Transform objectHolderPosition;        //A position to keep your valuable items.


    // Update is called once per frame
    void Update()
    {                      
            if (Input.GetMouseButtonDown(0) && !hasItem)
            {
              var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              RaycastHit hit;
              
                if (Physics.Raycast(ray, out hit) && !hasItem)
                {
                  PickUp(hit.collider.gameObject);
                }

            }

            if(Input.GetMouseButtonUp(0))
            {
                hasItem = false;
            }

             if (hasItem == true)
             {
                  item.transform.position = objectHolderPosition.transform.position;                                            
             }
                  
    }

    void PickUp(GameObject gameobject)
    {
        if (gameobject.GetComponent<Interactable>() !=null)
        {
            hasItem = true;
            item = gameobject;
            
        }
    }

}
