using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithObjects : MonoBehaviour
{    //Mikkel stole Runes code. I take credit.
    public bool hasItem;
    private GameObject item;
    public GameObject point;
    public Transform objectHolderPosition, cam;        //A position to keep your valuable items.

    //Shop Related
    public GameObject ShopEnterText;
    public GameObject BuyStandardTowerButton;
    public Transform PointWhereBoughtStuffArePlaced;

    public void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0) && !hasItem)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                point.transform.position = hit.point;
                if (!hasItem)
                {
                    PickUp(hit.collider.gameObject);
                }
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            hasItem = false;
            item.GetComponent<Rigidbody>().useGravity = true;
        }

        if (hasItem == true)
        {
            item.transform.position = objectHolderPosition.transform.position;

              if(Input.mouseScrollDelta.x !=0)
              {
                objectHolderPosition.transform.position -= cam.transform.forward * Time.deltaTime * Input.mouseScrollDelta.x * 50;
              }
            else if (Input.mouseScrollDelta.y != 0)
            {
                objectHolderPosition.transform.position += cam.transform.forward * Time.deltaTime * Input.mouseScrollDelta.y * 50;
            }
               
        }
           
        //Start shopstuffhere.
        if(Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == ("Vendor"))
            {
                Debug.Log("Entered Shop");
                ShopEnterText.SetActive(true);
                BuyStandardTowerButton.SetActive(true);

                Cursor.lockState = CursorLockMode.None;

            }

        }


    }

    void PickUp(GameObject gameobject)
    {
        if (gameobject.GetComponent<Interactable>() != null)
        {
            item = gameobject;
            item.GetComponent<Rigidbody>().useGravity = false;
            hasItem = true;

        }

    }
    



}
