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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
