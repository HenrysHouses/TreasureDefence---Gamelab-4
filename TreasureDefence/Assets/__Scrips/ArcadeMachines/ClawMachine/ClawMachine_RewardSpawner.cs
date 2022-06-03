using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMachine_RewardSpawner : MonoBehaviour
{
    [SerializeField] ArcadeMachine arcade;
    [SerializeField] List<GameObject> reward = new List<GameObject>();
    [SerializeField] StudioEventEmitter dispenceTowerSFX;

    // Update is called once per frame
    void Update()
    {
        if(arcade.isPlaying && reward.Count > 0)
        {
            foreach(GameObject found in reward)
            {
                GameManager.instance.SpawnTower(found, transform.position);
            }
            reward = new List<GameObject>();
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CrabGrabber"))
        {
            reward.Add(other.GetComponent<ClawMachine_rewardController>().reward);
            Destroy(other.gameObject);
            dispenceTowerSFX.Play();
        }
    }
}
