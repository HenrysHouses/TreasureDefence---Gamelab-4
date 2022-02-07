using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterRattle_EnemyBehaviour : EnemyBehaviour
{
	WaveController controller;
	public GameObject ChildPrefab;
	public int NumInCluster;
	
	public float spawnOffset;
	
	new private void Start()
	{
		base.Start();
		controller = transform.parent.parent.GetComponentInChildren<WaveController>();
	}
	
	public override void DeathRattle()
	{
		for (int i = 0; i < NumInCluster; i++)
		{
			Transform spawn = Instantiate(ChildPrefab).transform;
			spawn.SetParent(controller.EnemyHolder, true);
			EnemyBehaviour enemy = spawn.GetComponent<EnemyBehaviour>();
			enemy.GetProgress = GetProgress;
			WaveController.instance.AddEnemy(enemy);
		}
	}
	public override void EnemyUpdate(){}
	public override void AnimTrigger(){}
	
}
