/*
 * Written by:
 * Henrik
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using TMPro;

public class WackAMoleController_ArcadeMachine : ArcadeMachine
{
	[SerializeField] TextMeshPro _RemainingText;
	[SerializeField] string _StartText;
	[SerializeField] StudioEventEmitter winSound;
	[SerializeField] ParticleSystem[] confetti;
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
	[SerializeField] float TotalPlayTime  = 20;
	[SerializeField] float remainingTime;
	public bool isHoldingMallet;
	// Base Arcade Behaviour
	new void Start()
	{
		base.Start();
		_StartText = _RemainingText.text;
	}

	public override void isPlayingUpdate()
	{
		remainingTime -= Time.deltaTime;
		_RemainingText.text = "Remaining Time \n"+ (int)remainingTime;
		removeInActive();
			
		if(!cooldown)
			StartCoroutine(MolePopUp());
	}
	
	public override bool WinCondition()
	{
		if(hitCount >= WinningHits)
		{
			return true;
		}
		return false;
	}
	public override bool LoseCondition()
	{
		if(remainingTime < 0)
		{
			return true;
		}
		return false;
	}

	public override void LoseTrigger()
	{
		_RemainingText.text = "You lose";
	}

	public override void Reward()
	{
		// particle confetti feedback
		foreach (var particle in confetti)
		{
			particle.Play();
		}
		winSound.Play();
		_RemainingText.text = "You Won!";
		GameManager.instance.SpawnTower(towerRewardPrefab, spawnPoint.position);
	}
	public override void Reset()
	{
		hitCount = 0;
		StartCoroutine(resetInfo());
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

	public override void StartSetup()
	{
		remainingTime = TotalPlayTime;
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

	public void setIsHoldingMallet(bool state)
	{
		isHoldingMallet = state;
	}

	IEnumerator resetInfo()
	{
		yield return new WaitForSeconds(5);
		_RemainingText.text = _StartText;
	}
}
