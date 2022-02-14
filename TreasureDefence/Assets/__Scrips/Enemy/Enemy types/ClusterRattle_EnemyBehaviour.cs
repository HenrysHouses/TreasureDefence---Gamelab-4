using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterRattle_EnemyBehaviour : EnemyBehaviour
{
	public GameObject ChildPrefab;
	public int NumInCluster;
	
	public float spawnOffset;
	
	public override void DeathRattle()
	{
		for (int i = 0; i < NumInCluster; i++)
		{
			Transform spawn = Instantiate(ChildPrefab).transform;
			spawn.SetParent(waveController.EnemyHolder, true);
			EnemyBehaviour enemy = spawn.GetComponent<EnemyBehaviour>();
			enemy.GetProgress = GetProgress;
			waveController.AddEnemy(enemy);
		}
	}
	public override void EnemyUpdate(){}
	public override void AnimTrigger(){}
	
}
