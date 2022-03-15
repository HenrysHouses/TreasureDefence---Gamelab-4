/*
 * Written by:
 * Henrik
*/

using System;
using System.Collections.Generic;
using UnityEngine;

public class Base_TowerBehaviour : TowerBehaviour
{

	List<Base_TowerBehaviour> towers;

	new private void Start()
	{
		base.Start();
		GameManager.instance.AddTowerToList(gameObject);
	}

	override public void Attack(int damage, EnemyBehaviour[] targets)
	{
		if (targets != null)
		{
			_AudioSource.Play();
			Transform _projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, Quaternion.identity).transform;
			attackData newProjectile = getCurrentAttackData(_projectile, targets);
			projectile.Add(newProjectile);
		}
	}
	
	override public void projectileUpdate()
	{
		List<attackData> removeProjectiles = new List<attackData>();
		foreach (var currentProjectile in projectile)
		{
			Vector3 pos = currentProjectile.transform.position;
			if(currentProjectile.enemyPriority[currentProjectile.enemyPriority.Length-1])
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
			if (ExplotionParticle != null && deletingProjectile != null)
			{
				if(deletingProjectile.CurrentTarget)
				{
					Vector3 dir = transform.position - deletingProjectile.CurrentTarget.transform.position;
					Instantiate(ExplotionParticle, deletingProjectile.transform.position, Quaternion.LookRotation(dir)); 
				}
			}

			if(deletingProjectile.hit)
			{
				deletingProjectile.CurrentTarget.TakeDamage(deletingProjectile.projectileDamage);
				Debug.Log(gameObject.name + " dealt damage");

			}

			projectile.Remove(deletingProjectile);
			Destroy(deletingProjectile.gameObject);
		}
	}
}