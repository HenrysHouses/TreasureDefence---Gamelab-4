using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TreasureDefence/New Enemy Stats")]
public class EnemyInfo : ScriptableObject
{
	public float health;
	public float armor;
	public float speed;
	public int damage;
	public float attackCooldown;

	public int priority;
	public int moneyReward;

	public Mesh[] meshes;
}
