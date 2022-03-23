using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDeployable : TowerBehaviour
{
    // Start is called before the first frame update
    public int Damage, Radius;

	override public void Update()
	{
		
	}

	override public void projectileUpdate()
    {

    }

	override public void Attack(int damage, EnemyBehaviour[] targets)
    {
		 if (targets != null)
		 {
			Transform _Detonation = Instantiate(ExplotionParticle, projectileSpawnPos.position, Quaternion.identity).transform;
			attackData newDetonation = getCurrentAttackData(_Detonation, targets);
			projectile.Add(newDetonation);
         }
    }

	override public attackData getCurrentAttackData(Transform projectile, EnemyBehaviour[] targetPriority)
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




}
