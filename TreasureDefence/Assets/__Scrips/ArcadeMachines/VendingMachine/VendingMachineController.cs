using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VendingMachineController : MonoBehaviour
{
    [SerializeField] VendingMachineItem[] PurchasableItems;
    public Transform SpawnPoint;
    [SerializeField] TextMeshPro inputText; 
    public void buyTower()
    {
        
        foreach(var item in PurchasableItems)
        {
            if(item.ID == inputText.text)
            {
                if(CurrencyManager.instance.SubtractMoney(item.cost))
                {
                    GameObject spawn = Instantiate(item.ItemPrefab, SpawnPoint.position, Quaternion.identity);
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
