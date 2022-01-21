using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PathController path;
    
    public EnemyStats enemyStats;
    
    public EnemyScriptableObject eso;
    
    // private float health;
    // private float armor;
    // private float speed;
    // private int priority;
    // 
    
    void Start()
    {
        enemyStats = eso.enemyStats;
    }

    
    void Update()
    {
        //transform.position = path.GetP
    }
}

