using System.Collections.Generic;
using UnityEngine;

abstract public class TowerBehaviour : MonoBehaviour
{
	public enum TargetType
	{
		First,
		Closest,
		Strongest,
		Last
	}
	
	public TargetType targetType;
	public Transform projectileSpawnPos;
	public GameObject projectilePrefab;
	public GameObject ExplotionParticle;
	public bool canShoot = true;
	public int attackDamage;
	public float attackCooldown, attackSpeed;
	float Cooldown;
	public float towerRange;
	public List<attackData> projectile = new List<attackData>();
	private List<float> enemyProgress = new List<float>();
	public EnemyBehaviour enemyTarget;
	
	virtual public void Update()
	{	
		if(canShoot && WaveController.instance)
		{
			enemyTarget = CheckInRange(targetType);

			Cooldown -= Time.deltaTime;
			
			if(Cooldown < 0)
			{
				Attack(attackDamage, enemyTarget);
				Cooldown = attackCooldown;
			}
		}
		projectileUpdate();
	}
	
	abstract public void Attack(int damage, EnemyBehaviour target);
	
	abstract public void projectileUpdate();
	
	EnemyBehaviour CheckInRange(TargetType type)
	{
		float progress = Mathf.Infinity;
		float minimum = Mathf.Infinity;
		int index = -1;
		
		switch (type)
		{
			case TargetType.First:
				// Finding first enemy

				progress = 0f;
				
				for (int i = 0; i < WaveController.instance.enemies.Count; i++)
				{
					float dist = Vector3.Distance(transform.position, WaveController.instance.GetPosOfEnemy(i));
			
					if (dist < towerRange)
					{
						if (WaveController.instance.GetProgressOfEnemy(i) > progress)
						{
							progress = WaveController.instance.GetProgressOfEnemy(i);
							index = i;
						}
					}
				}
				// if a target was found
				if (index != -1)
					return WaveController.instance.enemies[index];
				
				break;
			
			case TargetType.Closest:
				// Finding closest enemy
				for (int i = 0; i < WaveController.instance.enemies.Count; i++)
				{
					float dist = Vector3.Distance(transform.position, WaveController.instance.GetPosOfEnemy(i));
			
					if (dist < towerRange)
					{
						if (dist < minimum)
						{
							minimum = dist;
							index = i;
						}
					}
				}
				// if a target was found
				if (index != -1)
					return WaveController.instance.enemies[index];
				
				break;
			
			case TargetType.Strongest:
				// Doesn't work yet.
				Debug.LogWarning("TargetType 'Strongest' is not implemented.");
				break;
			
			case TargetType.Last:
				// Finding last enemy
				for (int i = 0; i < WaveController.instance.enemies.Count; i++)
				{
					float dist = Vector3.Distance(transform.position, WaveController.instance.GetPosOfEnemy(i));
			
					if (dist < towerRange)
					{
						if (WaveController.instance.GetProgressOfEnemy(i) < progress)
						{
							progress = WaveController.instance.GetProgressOfEnemy(i);
							
							index = i;
						}
					}
				}
				// if a target was found
				if (index != -1)
					return WaveController.instance.enemies[index];
				
				break;
		}
		return null; // No enemies in range
	}
	
	public attackData getCurrentAttackData(Transform projectile, EnemyBehaviour target)
	{
		attackData data = new attackData();
		data.gameObject = projectile.gameObject;
		data.enemy = target;
		data.transform = projectile;
		data.target = target.transform;
		data.startPos = projectile.position;
		data.projectileSpeed = attackSpeed;
		data.projectileDamage = attackDamage;
		return data;
	}
}

public class attackData
{
	public GameObject gameObject;
	public Transform transform;
	public EnemyBehaviour enemy;
	public Transform target;
	public Vector3 startPos;
	public float projectileSpeed;
	public int projectileDamage;
	public float t;
	public bool hit;

	public Vector3 UpdateProjectile()
	{
		t = t + Time.deltaTime * projectileSpeed;
		t = Mathf.Clamp(t, 0, 1);
		if(t >= 1)
		{
			hit = true;
			return target.position;
		}
		return Vector3.Slerp(startPos, target.position, t);
	}
}