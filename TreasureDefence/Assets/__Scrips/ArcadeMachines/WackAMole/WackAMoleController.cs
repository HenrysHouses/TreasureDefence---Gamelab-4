/*
 * Written by:
 * Henrik
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WackAMoleController : ArcadeMachine
{
	[SerializeField] MoleController[] moles;
	List<int> activeMoles = new List<int>();
	
	[Header("Difficulty Stats")]
	[SerializeField] int simultaneouslyActiveMax;
	[SerializeField] float rangeTrigger, rangeMax;
	[SerializeField] float cooldownRangeMax, cooldownRangeMin;
	bool cooldown;
	
	[Header("Winning Condition")]
	public int hitCount;
	[SerializeField] int WinningHits;
	
	// Base Arcade Behaviour
	public override void isPlayingUpdate()
	{
		removeInActive();
			
		if(!cooldown)
			StartCoroutine(MolePopUp());
	}
	
	public override bool WinCondition()
	{
		if(hitCount >= WinningHits)
			return true;
		return false;
	}
	public override bool LooseCondition()
	{
		return false;
	}
	public override void Reward()
	{
		GameManager.instance.SpawnTower(towerRewardPrefab, spawnPoint.position);
	}
	public override void Reset()
	{
		hitCount = 0;
	}

	// Wack A Mole Behaviour
	void removeInActive()
	{
		List<int> remove = new List<int>();
		for (int i = 0; i < activeMoles.Count; i++)
		{
			if(!moles[activeMoles[i]].isMoving || !moles[activeMoles[i]].isHit)
			{
				remove.Add(activeMoles[i]);
			}
		}
		foreach (var item in remove)
			activeMoles.Remove(item);
	}

	IEnumerator MolePopUp()
	{
		cooldown = true;
		float randomCooldown = Random.Range(cooldownRangeMin, cooldownRangeMax);
		float randomTrigger = Random.Range(0, rangeMax);
		if(randomTrigger > rangeTrigger && simultaneouslyActiveMax > activeMoles.Count)
		{
			int selected = Random.Range(0, moles.Length); 
			if(!moles[selected].isMoving)
			{
				activeMoles.Add(selected);
				moles[selected].showMole();
			}
		}
		yield return new WaitForSeconds(randomCooldown);
		cooldown = false;
	}
}
