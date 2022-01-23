using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform pathPZero;

    public int amountToSpawn;

    public float spawnDelay;

    public GameObject[] enemies;

    private float counter;

    private int amountSpawned;
    
    void Start()
    {
        counter = Time.time + 2f;
    }

    void FixedUpdate()
    {
        if (Time.time > counter)
        {
            counter = Time.time + spawnDelay;

            SpawnEnemy(0);
        }
    }

    void SpawnEnemy(int enemyType)
    {
        if (amountSpawned <= amountToSpawn)
        {
            amountSpawned++;

            GameObject temp = Instantiate(enemies[enemyType], pathPZero.position, Quaternion.identity);

            GameManager.instance.AddEnemy(temp.GetComponent<Enemy>());
        }
    }
}
