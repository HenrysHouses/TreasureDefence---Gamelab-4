using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject StandardTower;
    public Transform SpawnLocation;

    public void SpawnTowerAtPoint()
    {
        Instantiate(StandardTower, SpawnLocation.position, Quaternion.identity);
        Cursor.lockState = CursorLockMode.Locked;
    }

  
}
