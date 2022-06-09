using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Henrik & Mikkel.
public class Tower_Monkey : TowerBehaviour
{									
	public float ProjectileLifeTime, Dot_Interval;
	[SerializeField] GameObject MonkeyMesh;
	public Animator MonkeyAnimator;
	public List<GameObject> Monkeys;

	override public bool Attack(int damage, EnemyBehaviour[] targets)
	{
		MonkeyAnimator.SetBool("MonkeyFalling", true);
		// Debug.Log("MOOOOOOOOOONNNKKEEEYYYFALL");
		bool attacked = false;
		if (targets != null)
        {
			//Instantiating projectile & Giving them the base stat from attackData.

			List<EnemyBehaviour> unaffectedTargets = new List<EnemyBehaviour>();
			for (int i = 0; i < targets.Length; i++)
			{
				bool isDebuffed = false;
				foreach(var debuff in targets[i].Debuffs)
				{
					if(debuff.GetType() == typeof(MonkeyDot))
					{
						isDebuffed = true;
					}
				}
				if (isDebuffed)
				{
					//MonkeyAnimator.SetBool("MonkeyDancing", true);	 Animation didn't happen.
					//Debug.Log("MOOOOOOONNNNKEEEEYYYYDAAANCCEE");
					continue;
				}
				unaffectedTargets.Add(targets[i]);
			}
			EnemyBehaviour[] currentValidTargets = unaffectedTargets.ToArray();
			if(currentValidTargets.Length > 0)
			{
				_AudioSource.Play();
				Transform _projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, Quaternion.identity).transform;
				attackData newProjectile = getCurrentAttackData(_projectile, currentValidTargets);
				//Adds the instantiated projectile to custom list.
				projectile.Add(newProjectile);
				attacked = true;
			}
		}
		return attacked;
	}

	override public void projectileUpdate()
	{
		List<attackData> removeProjectiles = new List<attackData>();
		foreach (var currentProjectile in projectile)
		{
			Vector3 pos = currentProjectile.transform.position;
			if(currentProjectile.enemyPriority.Length > 0)
			{
				if (currentProjectile.enemyPriority[currentProjectile.enemyPriority.Length-1])
					pos = currentProjectile.UpdateProjectile();
				else
				{
					removeProjectiles.Add(currentProjectile);
				}
			}

			//Checking if hit. Then update position.
			if (!currentProjectile.hit)
				currentProjectile.transform.position = pos;
			else
				removeProjectiles.Add(currentProjectile);
		}
		foreach (var deletingProjectile in removeProjectiles)		   //This happens for every projectile in the list.
		{
			// deletingProjectile.print();
			if(deletingProjectile.hit)
			{
				MonkeyDot debuff = new MonkeyDot();
				debuff.DotDamage = attackDamage;
				debuff.DotInterval = Dot_Interval;
				debuff.monkeyObject = Instantiate(MonkeyMesh).transform;
				addDebuff(deletingProjectile.CurrentTarget, debuff, ProjectileLifeTime);
			}
			// Instantiate(ExplotionParticle, deletingProjectile.transform.position, Quaternion.identity);
			projectile.Remove(deletingProjectile);
			Destroy(deletingProjectile.gameObject);
		}
	}
}
