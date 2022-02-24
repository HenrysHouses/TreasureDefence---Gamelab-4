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
		List<attackData> removeProjectiles = new List<attackData>();
		foreach (var currentProjectile in projectile)
		{
			Vector3 pos = currentProjectile.transform.position;
			if(currentProjectile.enemy)
				pos = currentProjectile.UpdateProjectile();
			else
			{
				removeProjectiles.Add(currentProjectile);
			}
			
			if(!currentProjectile.hit)
				currentProjectile.transform.position = pos;
			else
			{
				removeProjectiles.Add(currentProjectile);
			}
		}
		foreach (var deletingProjectile in removeProjectiles)
		{
			// deletingProjectile.print();
			Instantiate(ExplotionParticle, deletingProjectile.transform.position, Quaternion.identity); 

			if(deletingProjectile.hit)
				deletingProjectile.enemy.TakeDamage(deletingProjectile.projectileDamage);

			projectile.Remove(deletingProjectile);
			Destroy(deletingProjectile.gameObject);
		}
	}

}
