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
		Debug.LogError("I think this needs to be double checked if these targeting methods is actually finding a correctly sorted list - Henrik");
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
					Attack(attackDamage, enemyTarget);
					Cooldown = attackCooldown;
				}
			}
		}
		projectileUpdate();
	}
	
	/// <summary>Start an attack targeting an enemy with a projectile damage</summary>
	/// <param name="damage">The total damage of the projectile fired</param>
	/// <param name="targets">Enemy targeted by this projectile</param>
	abstract public void Attack(int damage, EnemyBehaviour[] targets);
	
	/// <summary>Called at the end of the Tower Update</summary>
	abstract public void projectileUpdate();
	
	EnemyBehaviour[] CheckInRange(TargetType type)
	{
		float progress = Mathf.Infinity;
		float minimum = Mathf.Infinity;
		
		List<EnemyBehaviour> enemiesInRange = new List<EnemyBehaviour>();

		switch (type)
		{
			case TargetType.First:
				// Finding first enemy

				progress = 0f;
				for (int i = 0; i < waveController.enemies.Count; i++)
				{
					float dist = Vector3.Distance(transform.position, waveController.GetPosOfEnemy(i));
			
					if (dist < towerRange)
					{
						/*if (waveController.GetProgressOfEnemy(i) > progress)
						{
							progress = waveController.GetProgressOfEnemy(i);
						}	 */
						enemiesInRange.Add(waveController.enemies[i]);
					}
				}
				// if a target was found
				if (enemiesInRange.Count > 0)
					return enemiesInRange.ToArray();
				
				break;
			
			case TargetType.Closest:
				// Finding closest enemy 
				for (int i = 0; i < waveController.enemies.Count; i++)
				{
					float dist = Vector3.Distance(transform.position, waveController.GetPosOfEnemy(i));
			
					if (dist < towerRange)
					{
						if (dist < minimum)
						{
							minimum = dist;
							enemiesInRange.Add(waveController.enemies[i]);
						}
					}
				}
				// if a target was found
				if (enemiesInRange.Count > 0)
					return enemiesInRange.ToArray();
				
				break;
			
			case TargetType.Strongest:
				// Doesn't work yet.
				Debug.LogWarning("TargetType 'Strongest' is not implemented.");
				break;
			
			case TargetType.Last:
				// Finding last enemy
				for (int i = 0; i < waveController.enemies.Count; i++)
				{
					float dist = Vector3.Distance(transform.position, waveController.GetPosOfEnemy(i));
			
					if (dist < towerRange)
					{
						if (waveController.GetProgressOfEnemy(i) < progress)
						{
							progress = waveController.GetProgressOfEnemy(i);

							enemiesInRange.Add(waveController.enemies[i]);
						}
					}
				}
				// if a target was found
				if (enemiesInRange.Count > 0)
					return enemiesInRange.ToArray();
				
				break;
		}
		return null; // No enemies in range
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
		dustCloud.Play();
    }

	void OnDestroy()
	{
		foreach (var attack in projectile)
		{
			Destroy(attack.gameObject);
		}
	}
}