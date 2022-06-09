using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDispenserController : MonoBehaviour
{
	[SerializeField] Transform repositionSpawnPoint, TemporaryTowerPosition;
    [SerializeField] Transform[] VisiblePositions;
    [SerializeField] List<GameObject> StoredTowers = new List<GameObject>();
    Transform DispensedTower;

    [SerializeField] StudioEventEmitter DispenceTowerSFX;
    public void fillDispenser(List<GameObject> towers)
    {
        for (int i = 0; i < towers.Count; i++)
        {
            if(!towers[i].GetComponent<MineDeployable>())
            {
                StoredTowers.Add(towers[i]);
                // Debug.Log("repositioned");
                towers[i].transform.position = TemporaryTowerPosition.position;
                towers[i].GetComponent<Collider>().enabled = false;
                if(towers.Count - i < VisiblePositions.Length+1)
                {
                    towers[i].transform.position = VisiblePositions[towers.Count - i-1].position;
                    towers[i].transform.rotation = VisiblePositions[towers.Count - i-1].rotation;
                    towers[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
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
            StoredTowers[0].GetComponent<Collider>().enabled = true;
            StoredTowers[0].transform.rotation = Quaternion.identity;
            StoredTowers.RemoveAt(0);

            if (!FmodExtensions.IsPlaying(DispenceTowerSFX.EventInstance))
            {
                DispenceTowerSFX.Play();
            }
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
