using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "TestSO")]
public class SO_Test : ScriptableObject
{
    public float health;
    public float armor;
    public float speed;

    public int priority;

    public Mesh[] meshes;
}
