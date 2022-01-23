using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    
    public Transform pathPZero;

    public int amountToSpawn;

    public float spawnDelay;

    public GameObject[] enemies;

    private float counter;

    private int amountSpawned;

    private bool waveStarted;
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        counter = Time.time + 2f;
    }

    void FixedUpdate()
    {
        if (waveStarted)
        {
            if (Time.time > counter)
            {
                counter = Time.time + spawnDelay;

                SpawnEnemy(0);
            }
        }
    }

    void SpawnEnemy(int enemyType)
    {
        if (amountSpawned < amountToSpawn)
        {
            amountSpawned++;

            GameObject temp = Instantiate(enemies[enemyType], pathPZero.position, Quaternion.identity);

            GameManager.instance.AddEnemy(temp.GetComponent<Enemy>());
        }
        else
        {
            GameManager.instance.EndWave();
            waveStarted = false;
        }
    }

    public void StartWave(int enemyAmount)
    {
        amountToSpawn = enemyAmount;
        amountSpawned = 0;

        counter = Time.time + spawnDelay;
        
        waveStarted = true;

    }
}
