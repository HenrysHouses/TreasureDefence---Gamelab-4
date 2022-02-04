using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "EnemySO")]
public class EnemyScriptableObject : ScriptableObject
{
    // [SerializeField]
    // public EnemyStats enemyStats;
}

[SerializeField]
public struct EnemyStats
{
    public float health;
    public float armor;
    public float speed;

    public int priority;

    public Mesh[] meshes;
}