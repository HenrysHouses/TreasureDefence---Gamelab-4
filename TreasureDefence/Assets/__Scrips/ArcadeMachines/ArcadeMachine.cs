/*
 * Written by:
 * Henrik
*/

using UnityEngine;

public abstract class ArcadeMachine : MonoBehaviour
{
	[Header("Base Arcade Variables")]
	public Transform spawnPoint;
	public GameObject towerRewardPrefab;
	[SerializeField] int Cost;
	public bool isPlaying;
	
	void Update()
	{
		if(isPlaying)
			isPlayingUpdate();
		
		if(WinCondition())
		{
			Reward();
			Reset();
			isPlaying = false;			
		}
		
		if(LooseCondition())
		{
			Reset();
			isPlaying = false;			
		}
	}

	public void StartGame()
	{
		if(CurrencyManager.instance.SubtractMoney(Cost) && !isPlaying)
		{
			isPlaying  = true;
			StartSetup();			
		}
	}
	
	public abstract void isPlayingUpdate();
	public abstract bool WinCondition();
	public abstract bool LooseCondition();
	public abstract void Reward();
	public abstract void Reset();
	public virtual void StartSetup(){}
}
