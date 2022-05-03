using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDispenserController : MonoBehaviour
{
	[SerializeField] Transform repositionSpawnPoint, TemporaryTowerPosition;
    [SerializeField] List<GameObject> StoredTowers = new List<GameObject>();
    Transform DispensedTower;

    public void fillDispenser(List<GameObject> towers)
    {
        for (int i = 0; i < towers.Count; i++)
        {
            if(!towers[i].GetComponent<MineDeployable>())
            {
                StoredTowers.Add(towers[i]);
                Debug.Log("repositioned");
                towers[i].transform.position = TemporaryTowerPosition.position;
            }
        }

        dispenseTower();
    }

    public void dispenseTower()
    {
        if(StoredTowers.Count > 0)
        {
            DispensedTower = StoredTowers[0].transform; 
            StoredTowers[0].transform.position = repositionSpawnPoint.position;
            StoredTowers.RemoveAt(0);
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if(other.transform == DispensedTower)
        {
            dispenseTower();
        }
    }
}
