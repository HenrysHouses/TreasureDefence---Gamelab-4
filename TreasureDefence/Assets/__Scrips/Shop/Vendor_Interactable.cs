using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vendor_Interactable : Interactable
{   //ShopManager
    public int totalCost;
    TowerInfo ti;
    public ShopManager shopManager;

    //CurrencyManager
    public static CurrencyManager instance;
  

    override public void InteractTrigger(object target = null)
    {
        Debug.Log("ThisWorks!");
        PayForTower();
        
    }

    public void PayForTower()
    {
        for (int i = 0; i < shopManager.items.Count; i++)
        {
            totalCost += shopManager.items[i].cost;
        }

        if (CurrencyManager.instance.money >= totalCost)
        {
            CurrencyManager.instance.SubtractMoney(totalCost);
            Debug.Log("You're rich.");
            for (int i = 0; i < shopManager.Object.Count; i++)
            {
                shopManager.Object[i].GetComponent<TowerBehaviour>().canShoot = true;
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
