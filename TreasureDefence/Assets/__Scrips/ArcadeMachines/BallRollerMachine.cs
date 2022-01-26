using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRollerMachine : MonoBehaviour
{
    public GameObject[] TowerBalls;
    public Transform spawnBalls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            float TowerValue = Random.Range(0, 100);
            if (isInRange(TowerValue, 1, 3))
            {
                // Spawn rare Tower
            }

            if (isInRange(TowerValue, 4, 30))
            {
                // Spawn semi-rare tower
            }

            if (isInRange(TowerValue, 31, 100))
            {
                // Spawn common tower
            }



            Instantiate(TowerBalls[Random.Range(0, TowerBalls.Length)], spawnBalls.position, spawnBalls.rotation);
        }
    }

    bool isInRange(float value, float from, float to)
    {
        if (value > from && value < to)
        {
            return true;
        }

        return false;
    }
}
