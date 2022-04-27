using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TreasureDefence/New Enemy Stats")]
public class EnemyInfo : ScriptableObject
{
	public int health = 1;
	public float armor = 1;
	public float speed = 0.03f;
	public int damage = 1;
	public float attackCooldown = 1;

	public int priority = 0;
	public int moneyReward = 1;
	public int experienceReward = 1;
	
	public Mesh[] meshes;
}
