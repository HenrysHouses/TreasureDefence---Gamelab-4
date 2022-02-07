using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vendor_Interactable : Interactable
{   //ShopManager
    public int totalCost;
    TowerInfo ti;

    public List<TowerInfo> items = new List<TowerInfo>();
    public List<GameObject> Object = new List<GameObject>();        //A refference to the gameobject.
    public Collider ShopCollider;
    
  


    //CurrencyManager
    public static CurrencyManager instance;
  

    override public void InteractTrigger(object target = null)
    {
        Debug.Log("ThisWorks!");
        PayForTower();
        
    }

    public void PayForTower()
    {
        for (int i = 0; i < items.Count; i++)
        {
            totalCost += items[i].cost;
        }

        if (CurrencyManager.instance.money >= totalCost)
        {
            CurrencyManager.instance.SubtractMoney(totalCost);
            Debug.Log("You're rich.");
            for (int i = 0; i < Object.Count; i++)
            {
                Object[i].GetComponent<TowerAttack>().enabled = true;
            }

        }
        else
        {
            Debug.Log("You're poor.");
        }
        totalCost = 0;
    }

    override public void InteractionEndTrigger(object target = null)
    {
        

    }


}
