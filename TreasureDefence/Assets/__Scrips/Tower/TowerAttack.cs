using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

	public float attackTime;
	
	//public List<Transform> enemies = new List<Transform>();
	//private List<Transform> enemiesInRange = new List<Transform>();

	private List<float> enemyProgress = new List<float>();
	
	private EnemyBehaviour target;

	private void Start()
	{
		InvokeRepeating(nameof(Attack), 0f, attackTime);
		this.enabled = false;
	}

	void Update()
	{
		CheckInRange();

		if (target != null)
		{
			Vector3 temp = target.GetPosition();
			
			transform.LookAt(new Vector3(temp.x, transform.position.y, temp.z));
		}
	}

	void Attack()
	{
		if (target != null)
		{
			target.TakeDamage(1);
		}
	}
	
	void CheckInRange()
	{
		float progress = Mathf.Infinity;
		float minimum = Mathf.Infinity;
		int index = -1;
		
		
		// switch (targetType)
		// {
		// 	case TargetType.First:
		// 		// Finding first enemy

		// 		progress = 0f;
				
		// 		for (int i = 0; i < WaveController.instance.enemies.Count; i++)
		// 		{
		// 			float dist = Vector3.Distance(transform.position, WaveController.instance.GetPosOfEnemy(i));
			
		// 			if (dist < towerRange)
		// 			{
		// 				if (WaveController.instance.GetProgressOfEnemy(i) > progress)
		// 				{
		// 					progress = WaveController.instance.GetProgressOfEnemy(i);
		// 					index = i;
		// 				}
		// 			}
		// 			else
		// 			{
		// 				target = null;
		// 			}
		// 		}
				
		// 		if (index != -1)
		// 			target = WaveController.instance.enemies[index];
				
		// 		break;
			
		// 	case TargetType.Closest:
		// 		// Finding closest enemy
		// 		for (int i = 0; i < WaveController.instance.enemies.Count; i++)
		// 		{
		// 			float dist = Vector3.Distance(transform.position, WaveController.instance.GetPosOfEnemy(i));
			
		// 			if (dist < towerRange)
		// 			{
		// 				if (dist < minimum)
		// 				{
		// 					minimum = dist;
		// 					index = i;
		// 				}
		// 			}
		// 			else
		// 			{
		// 				target = null;
		// 			}
		// 		}
		// 		if (index != -1)
		// 			target = WaveController.instance.enemies[index];
				
		// 		break;
			
		// 	case TargetType.Strongest:
		// 		// Doesn't work yet.
		// 		Debug.LogWarning("TargetType 'Strongest' is not set up.");
		// 		break;
			
		// 	case TargetType.Last:
		// 		for (int i = 0; i < WaveController.instance.enemies.Count; i++)
		// 		{
		// 			float dist = Vector3.Distance(transform.position, WaveController.instance.GetPosOfEnemy(i));
			
		// 			if (dist < towerRange)
		// 			{
		// 				if (WaveController.instance.GetProgressOfEnemy(i) < progress)
		// 				{
		// 					progress = WaveController.instance.GetProgressOfEnemy(i);
							
		// 					index = i;
		// 				}
		// 			}
		// 			else
		// 			{
		// 				target = null;
		// 			}
		// 		}
		// 		if (index != -1)
		// 			target = WaveController.instance.enemies[index];
				
		// 		break;
		// }
	}
}

