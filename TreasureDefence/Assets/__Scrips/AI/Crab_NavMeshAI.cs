using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crab_NavMeshAI : MonoBehaviour
{
    [SerializeField] Transform RandomPositionConstrain_A, RandomPositionConstrain_B; 
    NavMeshAgent Agent;
    [SerializeField] float WalkingCooldown;
    float Timestamp;
    bool reachedDestination;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Timestamp = Time.time;        
    }

    // Update is called once per frame
    void Update()
    {
        WaveController waveController = GameManager.instance.GetWaveController(); 
        if(waveController)
        {
            if(waveController.waveIsPlaying)
            {
                if(!Agent.isStopped)
                    Agent.isStopped = true;
            }
            else
            {
                if(Agent.isStopped)
                    Agent.isStopped = false;
            }
        }

        if(Agent.remainingDistance < 0.1f && !reachedDestination)
        {
            reachedDestination = true;
            Timestamp = Time.time;
        }
        else if(Time.time - Timestamp > WalkingCooldown && reachedDestination)
        {
            Agent.SetDestination(getRandomPosition());
            reachedDestination = false;
        }
    }



    Vector3 getRandomPosition()
    {
        Vector3 posA = RandomPositionConstrain_A.position;
        Vector3 posB = RandomPositionConstrain_B.position;

        float x = Random.Range(posA.x, posB.x);
        float y = Random.Range(posA.y, posB.y);
        float z = Random.Range(posA.z, posB.z);

        Vector3 randomPos = new Vector3(x, y, z);
        return randomPos;
    }
}
