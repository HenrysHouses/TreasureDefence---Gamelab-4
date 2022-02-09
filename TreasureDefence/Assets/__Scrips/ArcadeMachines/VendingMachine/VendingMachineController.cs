using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VendingMachineController : MonoBehaviour
{
    [SerializeField] VendingMachineItem[] PurchasableItems;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] TextMeshPro inputText; 
    public void buyTower()
    {
        foreach(var item in PurchasableItems)
        {
            if(item.ID == inputText.text)
            {
                if(CurrencyManager.instance.money > item.cost)
                {
                    GameObject spawn = Instantiate(item.ItemPrefab, SpawnPoint.position, Quaternion.identity);      
                    CurrencyManager.instance.SubtractMoney(item.cost);
                }
                inputText.text = "";
                return;  
            }
        }
    }
}

[System.Serializable]
public struct VendingMachineItem
{
    public GameObject ItemPrefab;
    public string ID;
    public int cost;
}
