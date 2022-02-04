using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{ public int totalCost;
    TowerInfo ti;
    
    public List<TowerInfo> items = new List<TowerInfo>();
    public List<GameObject> Object = new List<GameObject>();        //A refference to the gameobject.

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ItemEnteredShopTrigger");
        ti = other.gameObject.GetComponent<Tower_Interactable>().towerInfo;

        if (ti != null)
        {
            items.Add(ti);
            //total = ti.cost;
            Object.Add(other.gameObject);  //When the tower collides with the trigger, it is put into the Object list. Allowing us to <Digg> for the Towerattack script.
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("ItemExitedShopTrigger");
        items.Remove(other.gameObject.GetComponent<Tower_Interactable>().towerInfo);
        Object.Remove(other.gameObject);

    }



    public void PayForTower()
    {
        for(int i = 0; i < items.Count; i++)
        {
            totalCost += items[i].cost;
        }

       if(CurrencyManager.instance.money >= totalCost)
        {
            CurrencyManager.instance.SubtractMoney(totalCost);
            Debug.Log("You're rich.");
            for (int i = 0; i < Object.Count; i++)
            {
                Object[i].GetComponent<TowerBehaviour>().canShoot = true;
            }

        }
       else
        {
            Debug.Log("You're poor.");
        }
        totalCost = 0;
    }

}
