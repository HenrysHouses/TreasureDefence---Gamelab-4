/*
 * Written by:
 * Henrik
*/
using System.Collections.Generic;
using UnityEngine;

public class Base_TowerBehaviour : TowerBehaviour
{

	List<Base_TowerBehaviour> towers;
	


	override public void Attack(int damage, EnemyBehaviour[] targets)
	{
		if (targets != null)
		{
			Transform _projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, Quaternion.identity).transform;
			attackData newProjectile = getCurrentAttackData(_projectile, targets[0]);
			projectile.Add(newProjectile);
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