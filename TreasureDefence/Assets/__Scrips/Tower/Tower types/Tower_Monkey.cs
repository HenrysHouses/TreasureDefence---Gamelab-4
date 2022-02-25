using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Henrik & Mikkel.
public class Tower_Monkey : TowerBehaviour
{									
	public float ProjectileLifeTime, Dot_Interval;

	List<attackData_DOT> projectileDOT = new List<attackData_DOT>();
	override public void Attack(int damage, EnemyBehaviour[] targets)
	{
		if (targets != null)
        {
			//Instatiating projectile & Giving them the base stat from attackData.
			Transform _projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, Quaternion.identity).transform;
			attackData newProjectile = getCurrentAttackData(_projectile, targets[0]);

			//Converting the stats from attackData to attackData_DOT.
			attackData_DOT attackData_DOTProjectile = new attackData_DOT();
			attackData_DOTProjectile.projectileDamage = newProjectile.projectileDamage;
			attackData_DOTProjectile.projectileSpeed = newProjectile.projectileSpeed;
			attackData_DOTProjectile.gameObject = newProjectile.gameObject;
			attackData_DOTProjectile.transform = newProjectile.transform;
			attackData_DOTProjectile.enemy = newProjectile.enemy;
			attackData_DOTProjectile.transform = newProjectile.target;
			attackData_DOTProjectile.startPos = newProjectile.startPos;
			attackData_DOTProjectile.hit = newProjectile.hit;

			attackData_DOTProjectile.Duration = ProjectileLifeTime;
			attackData_DOTProjectile.DamageInterval = Dot_Interval;
			
			//Adds the instantiated projectile to custom list.
			projectileDOT.Add(attackData_DOTProjectile);
		}
	}

	override public void projectileUpdate()
	{
		List<attackData_DOT> removeProjectiles = new List<attackData_DOT>();
		foreach (var currentProjectile in projectileDOT)
		{
			Vector3 pos = currentProjectile.transform.position;
			if (currentProjectile.enemy)
				pos = currentProjectile.UpdateProjectile();
			else
			{
				removeProjectiles.Add(currentProjectile);
			}

			//Checking if hit. Then update position.
			if (!currentProjectile.hit)
				currentProjectile.transform.position = pos;
			//Else if the duration has ended, it adds the currentprojectile to a list called removeProjectile.
			else	if(currentProjectile.DurationHasEnded)  
			{
				removeProjectiles.Add(currentProjectile);
			}

			//When the projectile hit the target, it will stay for a duration while following the targets position and deal damage over said time.
			if(currentProjectile.hit)
            {
				currentProjectile.transform.position = currentProjectile.target.position;


            }

		}
		foreach (var deletingProjectile in removeProjectiles)		   //This happens for every projectile in the list.
		{
			// deletingProjectile.print();
			Instantiate(ExplotionParticle, deletingProjectile.transform.position, Quaternion.identity);

			projectileDOT.Remove(deletingProjectile);
			Destroy(deletingProjectile.gameObject);
		}
	}

}
