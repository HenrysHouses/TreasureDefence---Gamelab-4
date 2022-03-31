using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_BombThrower : TowerBehaviour
{
	public int maxTargets;

	new private void Start()
	{
		base.Start();
		GameManager.instance.AddTowerToList(gameObject);
	}
	
    override public void Attack(int damage, EnemyBehaviour[] targets)
    {
		if (targets != null)
		{
			for( int i = 0; i < maxTargets; i++)
            {
				if (i >= targets.Length)
                {
					break;
                }

				_AudioSource.Play();

				EnemyBehaviour[] currentTargets = new EnemyBehaviour[targets.Length-i];

				for (int j = 0; j < currentTargets.Length; j++)
				{
					currentTargets[j] = targets[j+i];
				}


				Transform _projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, Quaternion.identity).transform;
				attackData newProjectile = getCurrentAttackData(_projectile, currentTargets);
				projectile.Add(newProjectile);

				// if(targets.Length > 1)
				// {
				// 	EnemyBehaviour[] _SecondaryTargets = new EnemyBehaviour[targets.Length-1];
					
				// 	for (int j = 0; j < _SecondaryTargets.Length-1; j++)
				// 	{
				// 		_SecondaryTargets[j] = targets[j+1];
				// 	}

				// 	Transform _projectile2 = Instantiate(projectilePrefab, projectileSpawnPos.position, Quaternion.identity).transform;
				// 	attackData newProjectile2 = getCurrentAttackData(_projectile2, _SecondaryTargets);
				// 	projectile.Add(newProjectile2);

				// 	foreach (var target in targets)
				// 	{
						
				// 	}
				// }
			}
		}
	}

	override public void projectileUpdate()
	{
		List<attackData> removeProjectiles = new List<attackData>();
		foreach (var currentProjectile in projectile)
		{
			bool skip = false;
			Vector3 pos = currentProjectile.transform.position;
			if(currentProjectile.enemyPriority[currentProjectile.enemyPriority.Length-1])
				pos = currentProjectile.UpdateProjectile();
			else
			{
				removeProjectiles.Add(currentProjectile);
				skip = true;
			}
			
			if(!skip)
			{
				if(!currentProjectile.hit)
				{
					currentProjectile.transform.position = pos;
					currentProjectile.transform.GetChild(0).LookAt(currentProjectile.CurrentTarget.transform, Vector3.up);
					Vector3 forward = currentProjectile.transform.TransformDirection(Vector3.right);
					Vector3 dir = currentProjectile.transform.position - currentProjectile.CurrentTarget.transform.position;

					if(Vector3.Dot(forward, dir) > 0.02) // target is on the left
					{
						currentProjectile.transform.Rotate(Vector3.up * Time.deltaTime * 200, Space.World);
					}
					else if(Vector3.Dot(forward, dir) < -0.02) // target is on the right
					{
						currentProjectile.transform.Rotate(-Vector3.up * Time.deltaTime * 200, Space.World);
					}
					currentProjectile.transform.Rotate(Vector3.right * Time.deltaTime * 400, Space.Self);
				}
				else
				{
					removeProjectiles.Add(currentProjectile);
				}
			}
		}
		foreach (var deletingProjectile in removeProjectiles)
		{
			// deletingProjectile.print();

			if(deletingProjectile.hit)
			{
				Instantiate(ExplotionParticle, deletingProjectile.transform.position, Quaternion.identity); 
				deletingProjectile.CurrentTarget.TakeDamage(deletingProjectile.projectileDamage);
			}

			projectile.Remove(deletingProjectile);
			Destroy(deletingProjectile.gameObject);
		}
	}
}
