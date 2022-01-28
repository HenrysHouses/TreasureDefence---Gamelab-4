using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{ public int total;
    TowerInfo ti;

    public List<TowerInfo> items = new List<TowerInfo>();  

    private void OnTriggerEnter(Collider other)
    {
        ti = other.gameObject.GetComponent<Tower_Interactable>().towerInfo;

        if (ti != null)
        {
            items.Add(ti); 
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        items.Remove(other.gameObject.GetComponent<Tower_Interactable>().towerInfo);
    

    }



    public void PayForTower()
    {
        for(int i = 0; i < items.Count; i++)
        {
            total += items[i].cost;
        }

       if(GameManager.instance.money >= total)
        {
            GameManager.instance.money -= total;
            Debug.Log("You're rich.");
            
        }
       else
        {
            Debug.Log("You're poor.");
        }
        total = 0;
    }

}
