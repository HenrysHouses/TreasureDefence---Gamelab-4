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
	public int _Cost => Cost;
	public bool isPlaying, hasReset;
	// StudioEventEmitter _Audiosource;
	[SerializeField] StudioEventEmitter _PayingforMinigamesSFX, winSFX, loseSFX;

	
	public void Start()
	{
		// _Audiosource = GetComponent<StudioEventEmitter>();
	}

	public void Update()
	{
		if(isPlaying)
			isPlayingUpdate();
		
		if(WinCondition() && isPlaying && !hasReset)
		{
			
			// _Audiosource.Play();
			Reward();
			Reset();
			if(winSFX)
				winSFX.Play();
			isPlaying = false;			
			hasReset = true;
		}
		
		if(LoseCondition() && isPlaying && !hasReset)  
		{
			LoseTrigger();
			Reset();
			if(loseSFX)
				loseSFX.Play();

			// ! temporary lost game sound
			loseSFX.SetParameter("Valid_Invalid", 1);


			isPlaying = false;	
			hasReset = true;		
		}
	}

	public void StartGame()
	{
		if(CurrencyManager.instance.SubtractMoney(Cost) && !isPlaying)
		{
			//PlaySound
			if (!FmodExtensions.IsPlaying(_PayingforMinigamesSFX.EventInstance)) // valid payment sound
			{
				_PayingforMinigamesSFX.Play();
				_PayingforMinigamesSFX.SetParameter("Valid_Invalid", 0);
			}
			isPlaying  = true;
			hasReset = false;
			StartSetup();			
		}
		else // invalid payment sound
        {
			_PayingforMinigamesSFX.Play();
			_PayingforMinigamesSFX.SetParameter("Valid_Invalid", 1);
		}
	}
	
	public abstract void isPlayingUpdate();
	public abstract bool WinCondition();
	public abstract bool LoseCondition();
	public abstract void LoseTrigger();
	public abstract void Reward();
	public abstract void Reset();
	public virtual void StartSetup(){}
}
