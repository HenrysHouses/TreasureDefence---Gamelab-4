using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class RaycastForInteractable : MonoBehaviour
{
    public string tag;

    [SerializeField]
    public LayerMask layerMask;
    
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.magenta);
        
        if (Physics.Raycast(ray, out hit, 2.5f, layerMask))
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
            
            
            /*
            tag = hit.transform.tag;
            
            Cup();
            */
            //Invoke("" + tag, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Cup();
    }

    void Cup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.LogWarning("Shot ray.");
            
            GameManager.instance.StartWave();
        }
    }

    void Vendor()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
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
