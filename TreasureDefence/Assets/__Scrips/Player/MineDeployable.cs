using System.Collections;
using System.Collections.Generic;
using UnityEngine;
			  //Mikkel did this.
public class MineDeployable : TowerBehaviour
{

	public int maxTargets;
	

    public override void Update()
    {
        base.Update();
		//canShoot = true;
		
	
    }

    new private void Start()
	{
		
		base.Start();
		GameManager.instance.AddTowerToList(gameObject);
	}

	override public void Attack(int damage, EnemyBehaviour[] targets)
	{
		if (targets != null)
		{
			
			for (int i = 0; i < maxTargets; i++)
			{
				
				if (i >= targets.Length)
				{
					break;
				}
				_AudioSource.Play();
				Transform _projectile =  new GameObject().transform;
				_projectile.position = projectileSpawnPos.position; 
				attackData newProjectile = getCurrentAttackData(_projectile, targets);
				projectile.Add(newProjectile);
			}
		}
	}

	override public void projectileUpdate()
	{
		List<attackData> removeProjectiles = new List<attackData>();
		foreach (var currentProjectile in projectile)
		{
			if(currentProjectile == null)
				removeProjectiles.Add(currentProjectile);

			Vector3 pos = currentProjectile.transform.position;
			if (currentProjectile.enemyPriority[currentProjectile.enemyPriority.Length - 1])
				pos = currentProjectile.UpdateProjectile();
			else
			{
				removeProjectiles.Add(currentProjectile);
			}

			if (!currentProjectile.hit)
			{
				currentProjectile.transform.position = pos;
			}
			else
			{
				removeProjectiles.Add(currentProjectile);
			}
		}
		foreach (var deletingProjectile in removeProjectiles)
		{
			// deletingProjectile.print();

			if (deletingProjectile.hit)
			{
				Instantiate(ExplotionParticle, deletingProjectile.transform.position, Quaternion.identity);
				deletingProjectile.CurrentTarget.TakeDamage(deletingProjectile.projectileDamage);
				Destroy(this.gameObject);
			}

			projectile.Remove(deletingProjectile);
			Destroy(deletingProjectile.gameObject);
		}
	}


}
