using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_GunMan : TowerBehaviour
{
	public int maxTargets;

	override public bool Attack(int damage, EnemyBehaviour[] targets)
	{
		return false;
	}

	override public void projectileUpdate()
	{
		
		
	}



}
