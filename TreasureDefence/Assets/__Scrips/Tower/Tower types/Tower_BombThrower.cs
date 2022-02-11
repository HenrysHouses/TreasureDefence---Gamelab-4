using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_BombThrower : TowerBehaviour
{
	public int maxTargets;

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
				Transform _projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, Quaternion.identity).transform;
				attackData newProjectile = getCurrentAttackData(_projectile, targets[i]);
				projectile.Add(newProjectile);
			}
		}
	}

	override public void projectileUpdate()
	{
		List<attackData> projectilesHit = new List<attackData>();
		foreach (var currentProjectile in projectile)
		{
			Vector3 pos = currentProjectile.UpdateProjectile();

			if (!currentProjectile.hit)
				currentProjectile.transform.position = pos;
			else
			{
				projectilesHit.Add(currentProjectile);
			}
		}
		foreach (var hit in projectilesHit)
		{
			Instantiate(ExplotionParticle, hit.transform.position, Quaternion.identity);
			hit.enemy.TakeDamage(hit.projectileDamage);
			projectile.Remove(hit);
			Destroy(hit.gameObject);
		}
	}

}
