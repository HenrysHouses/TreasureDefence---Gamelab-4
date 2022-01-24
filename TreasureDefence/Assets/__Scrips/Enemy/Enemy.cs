using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PathController path;
    
    public SO_Test enemyStats;

    private OrientedPoint op;

    public int health;
    
    public float scaleTime;
    
    //public EnemyScriptableObject eso;

    public MeshRenderer mr;
    
    public float progress;
    
    // private float health;
    // private float armor;
    // private float speed;
    // private int priority;
    // private Mesh[] meshes;

    private void Start()
    {
        transform.localScale = Vector3.zero;

        InvokeRepeating(nameof(InitialScale), 0f, Time.fixedDeltaTime);
    }

    void Update()
    {
        progress += GameManager.instance.windSpeed * Time.deltaTime;
        
        op = path.GetPathOP(enemyStats.speed * progress);

        transform.position = op.pos;
    }



    private void InitialScale()
    {
        transform.localScale += Vector3.one * scaleTime * Time.fixedDeltaTime;
        
        
        if (transform.localScale.x > 0.099f)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            CancelInvoke();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        mr.material.color = Color.red;
        
        Invoke(nameof(ResetColor), 0.1f);
        
        if (health <= 0)
        {
            Destroy(gameObject);    // Let's improve this at some point
        }
    }

    private void ResetColor()
    {
        mr.material.color = Color.white;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    
    public float GetProgress()
    {
        return progress;
    }
    
    private void OnDestroy()
    {
        GameManager.instance.RemoveEnemy(this);
    }
}


