using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTowerButton_Interactable : MonoBehaviour
{
    [SerializeField] GameObject TowerPrefab;
    TowerInfo info;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        info = TowerPrefab.GetComponent<Tower_Interactable>().towerInfo;
    }

}
