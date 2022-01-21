using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public enum TargetType
    {
        First,
        Closest,
        Strongest,
        Last
    }
    public TargetType targetType;
    
    public float towerRange;
    
    public List<Transform> enemies = new List<Transform>();
    //private List<Transform> enemiesInRange = new List<Transform>();

    private Transform target;
    
    void Update()
    {
        CheckInRange();

    }


    void Attack(Transform t)
    {
        
    }

    void Test()
    {
        if (target != null)
            target.localScale = new Vector3(Random.Range(0.03f, 0.2f), Random.Range(0.03f, 0.2f), Random.Range(0.03f, 0.2f));
    }
    
    void CheckInRange()
    {
        float minimum = Mathf.Infinity;
        int index = 99;

        switch (targetType)
        {
            case TargetType.First:
                for (int i = 0; i < enemies.Count; i++)
                {
                    float dist = Vector3.Distance(transform.position, enemies[i].position);
            
                    if (dist < towerRange)
                    {
                        target = enemies[i];
                        break;
                    }
                    else
                    {
                        target = null;
                    }
                }
                break;
            
            case TargetType.Closest:
                // Finding closest enemy
                for (int i = 0; i < enemies.Count; i++)
                {
                    float dist = Vector3.Distance(transform.position, enemies[i].position);
            
                    if (dist < towerRange)
                    {
                        if (dist < minimum)
                        {
                            minimum = dist;
                            index = i;
                        }
                    }
                    else
                    {
                        target = null;
                    }
                }
                if (index != 99)
                    target = enemies[index];
                
                break;
            
            case TargetType.Strongest:
                
                break;
            
            case TargetType.Last:
                for (int i = 0; i < enemies.Count; i++)
                {
                    float dist = Vector3.Distance(transform.position, enemies[i].position);
            
                    if (dist < towerRange)
                    {
                        target = enemies[i];
                        break;
                    }
                    else
                    {
                        target = null;
                    }
                }
                break;
        }

        Test();
        

        
        /*
        for (int i = 0; i < enemies.Count; i++)
        {
            if (Vector3.Distance(transform.position, enemies[i].position) < towerRange)
            {
                enemiesInRange.Add(enemies[i]);
                enemies.RemoveAt(i);
            }
        }

        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (Vector3.Distance(transform.position, enemiesInRange[i].position) > towerRange)
            {
                enemies.Add(enemiesInRange[i]);
                enemiesInRange.RemoveAt(i);
            }
        }
        */
    }
}

