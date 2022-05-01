using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_HarpoonCannon : TowerBehaviour
{
    [Header("Harpoon Variables")]
    [SerializeField] Transform HarpoonRotation;
	public float ProjectileLifeTime;
    public GameObject harpoonMesh;
    public Transform HarpoonRopePos;
    public float SlowStrength = 0.25f;
    public modifierType ModifierCalculationType = modifierType.multiplicative;
    override public bool Attack(int damage, EnemyBehaviour[] targets)
	{
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
					if(debuff.GetType() == typeof(Slow))
					{
						isDebuffed = true;
					}
				}
				if(isDebuffed)
					continue;
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
		if(projectile.Count > 0)
		{
			harpoonMesh.SetActive(false);
		}
		else if(!harpoonMesh.activeSelf)
			harpoonMesh.SetActive(true);


        if(enemyTarget != null)
        {
            if(enemyTarget.Length > 0)
            {
                Vector3 rot = HarpoonRotation.eulerAngles;
                HarpoonRotation.LookAt(enemyTarget[0].transform);
                Vector3 newRot = HarpoonRotation.eulerAngles;
                newRot.x = rot.x;
                newRot.z = rot.z;
            }
        }
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
				Slow debuff = new Slow();
                debuff.effect.value = SlowStrength;
                debuff.effect.type = ModifierCalculationType;
				debuff.gameObject = Instantiate(harpoonMesh);
				debuff.gameObject.SetActive(true);
				debuff.Transform = debuff.gameObject.transform;
				addDebuff(deletingProjectile.CurrentTarget, debuff, ProjectileLifeTime);
			}
			// Instantiate(ExplotionParticle, deletingProjectile.transform.position, Quaternion.identity);
			projectile.Remove(deletingProjectile);
			Destroy(deletingProjectile.gameObject);
		}
    }
}