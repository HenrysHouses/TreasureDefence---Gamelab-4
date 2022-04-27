/*
 * Written by:
 * Henrik
*/

using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
abstract public class TowerBehaviour : MonoBehaviour
{
	public enum TargetType
	{
		First,
		Closest,
		Strongest,
		Last
	}

	public StudioEventEmitter _AudioSource;
	public WaveController waveController;
	public TargetType targetType;
	public Transform projectileSpawnPos;
	public GameObject projectilePrefab;
	public GameObject ExplotionParticle;
	public bool canShoot = false;
	public int attackDamage;
	public float attackCooldown, attackSpeed;
	float Cooldown;
	public float towerRange;
	public List<attackData> projectile = new List<attackData>();
	private List<float> enemyProgress = new List<float>();
	public EnemyBehaviour[] enemyTarget;

	public ParticleSystem dustCloud;

	public void Start()
	{
		_AudioSource = GetComponent<StudioEventEmitter>();
	}

	virtual public void Update()
	{	
		waveController = GameManager.instance.GetWaveController();
		if(canShoot && waveController)
		{
			enemyTarget = CheckInRange(targetType);

			Cooldown -= Time.deltaTime;
			
			if(enemyTarget != null)
			{
				if(Cooldown < 0 && enemyTarget.Length > 0)
				{
					if(Attack((int)(attackDamage * GameManager.instance.damageMultiplier), enemyTarget))
                    {
                        Cooldown = attackCooldown / GameManager.instance.attSpeedMultiplier;
                    }
				}
			}
		}
		projectileUpdate();

	}
	
	/// <summary>Start an attack targeting an enemy with a projectile damage</summary>
	/// <param name="damage">The total damage of the projectile fired</param>
	/// <param name="targets">Enemy targeted by this projectile</param>
	abstract public bool Attack(int damage, EnemyBehaviour[] targets);
	
	/// <summary>Called at the end of the Tower Update</summary>
	abstract public void projectileUpdate();
	
	EnemyBehaviour[] CheckInRange(TargetType type)
	{
		List<EnemyBehaviour> inRange; // array of enemies within the tower range
		
		List<int> index = new List<int>(); // indexes added to enemyTargetPriority
		List<EnemyBehaviour> enemyTargetPriority = new List<EnemyBehaviour>(); // list of sorted enemies in range according to the targeting type

		switch (type)
		{
			case TargetType.First:
				// Finding first enemy

				enemyTargetPriority = FindEnemiesWithinRange();
				// if a target was found
				if (enemyTargetPriority.Count > 0)
					return enemyTargetPriority.ToArray();
				
				break;
			
			case TargetType.Closest:
				// Finding enemies in range
				inRange = FindEnemiesWithinRange();

				// Finding closest enemy
				if(inRange != null)
				{
					

					for (int i = 0; i < inRange.Count; i++)
					{
						int closestTargetIndex = 0;
						float currentClosestDist = Mathf.Infinity;
						for (int j = 0; j < inRange.Count; j++)
						{
							float checkDist = Vector3.Distance(transform.position, inRange[j].transform.position);

							if (checkDist < currentClosestDist && !enemyTargetPriority.Contains(inRange[j]))
							{
								closestTargetIndex = j;
								currentClosestDist =  Vector3.Distance(transform.position, inRange[closestTargetIndex].transform.position);
							}
						}
						if(!enemyTargetPriority.Contains(inRange[closestTargetIndex]))
						{
							enemyTargetPriority.Add(waveController.enemies[closestTargetIndex]);
							index.Add(closestTargetIndex);
						}
					}

					// if a target was found
					if (enemyTargetPriority.Count > 0)
					{
						return enemyTargetPriority.ToArray();
					}
				}
				break;
			
			case TargetType.Strongest:
				// Finding enemies in range
				inRange = FindEnemiesWithinRange();
				// If enemies are in range sort for strongest enemies
				if(inRange != null)
				{
					for (int i = 0; i < inRange.Count; i++)
					{
						int currentStrongestIndex = 0;
						for (int j = 0; j < inRange.Count; j++)
						{
							if(inRange[currentStrongestIndex].GetHealth < inRange[j].GetHealth && !enemyTargetPriority.Contains(inRange[j]))
							{
								currentStrongestIndex = j;
							}
						}
						if(!enemyTargetPriority.Contains(inRange[currentStrongestIndex]))
							enemyTargetPriority.Add(inRange[currentStrongestIndex]);
					}
					if (enemyTargetPriority.Count > 0)
						return enemyTargetPriority.ToArray();
				}
				break;
			
			case TargetType.Last:
				// Finding enemies in range
				inRange = FindEnemiesWithinRange();
				// Finding last enemy
				if(inRange != null)
				{
					inRange.Reverse();
					// if a target was found
					if (inRange.Count > 0)
					{
						return inRange.ToArray();
					}
				}
				break;
		}
		return null; // No enemies in range
	}

	bool IndexExists(int index, List<int> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if(list[i] == index)
				return true;
		}
		return false;
	}

	List<EnemyBehaviour> FindEnemiesWithinRange()
	{
		List<EnemyBehaviour> enemiesInRange = new List<EnemyBehaviour>();
		for (int i = 0; i < waveController.enemies.Count; i++)
		{
			float dist = Vector3.Distance(transform.position, waveController.GetPosOfEnemy(i));
	
			if (dist < towerRange)
			{
				enemiesInRange.Add(waveController.enemies[i]);
			}
		}
		return enemiesInRange;
	}
	
	virtual public attackData getCurrentAttackData(Transform projectile, EnemyBehaviour[] targetPriority)
	{
		attackData data = new attackData();
		data.gameObject = projectile.gameObject;
		data.enemyPriority = targetPriority;
		data.transform = projectile;
		data.startPos = projectile.position;
		data.projectileSpeed = attackSpeed;
		data.projectileDamage = attackDamage;
		return data;
	}
    private void OnCollisionEnter(Collision collision)
    {
		if(!dustCloud.isPlaying)
			dustCloud.Play();
    }

	void OnDestroy()
	{
		foreach (var attack in projectile)
		{
			Destroy(attack.gameObject);
		}
	}

	public void addDebuff(EnemyBehaviour target, EnemyDebuff debuff, float duration)
	{
		debuff.Duration = duration;
		debuff.Start();
		target.Debuffs.Add(debuff);
	}
}