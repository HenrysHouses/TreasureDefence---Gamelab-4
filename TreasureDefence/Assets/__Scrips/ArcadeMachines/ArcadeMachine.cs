/*
 * Written by:
 * Henrik
*/

using UnityEngine;
using FMODUnity;

// [RequireComponent(typeof(StudioEventEmitter))]
public abstract class ArcadeMachine : MonoBehaviour
{
	[Header("Base Arcade Variables")]
	public Transform spawnPoint;
	public GameObject towerRewardPrefab;
	[SerializeField] int Cost;
	public bool isPlaying, hasReset;
    // StudioEventEmitter _Audiosource;
	
	public void Start()
	{
		// _Audiosource = GetComponent<StudioEventEmitter>();
	}

	void Update()
	{
		if(isPlaying)
			isPlayingUpdate();
		
		if(WinCondition() && isPlaying && !hasReset)
		{
			
			// _Audiosource.Play();
			Reward();
			Reset();
			isPlaying = false;			
			hasReset = true;
		}
		
		if(LooseCondition() && !hasReset)
		{
			Reset();
			isPlaying = false;	
			hasReset = true;		
		}
	}

	public void StartGame()
	{
		if(CurrencyManager.instance.SubtractMoney(Cost) && !isPlaying)
		{
			isPlaying  = true;
			hasReset = false;
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
