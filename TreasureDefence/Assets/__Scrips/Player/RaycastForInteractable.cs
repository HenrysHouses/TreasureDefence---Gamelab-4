using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class RaycastForInteractable : MonoBehaviour
{
    public string tag;

    [SerializeField]
    public LayerMask layerMask;

    public GameObject vendorGreeting;
    public GameObject vendorMenu;

    public GameObject ePrompt;
    
    private GameObject item;
    private bool hasItem;

    public Transform objectHolderPosition;
    public Transform cam;
    
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.magenta);

        if (Physics.Raycast(ray, out hit, 2.7f, layerMask))
        {
            switch (hit.transform.tag)
            {
                case "Cup":
                    Cup();
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
            RaycastHit pickupHit;

            if (Physics.Raycast(ray, out hit))
            {
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
        
        if (Input.GetKeyDown(KeyCode.Space))
            Cup();
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
    
    void Clear()
    {
        vendorGreeting.SetActive(false);
        vendorMenu.SetActive(false);
        ePrompt.SetActive(false);
    }

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
                GameObject.FindGameObjectWithTag("Vendor").GetComponent<ShopManager>().SpawnTowerAtPoint();
            }
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