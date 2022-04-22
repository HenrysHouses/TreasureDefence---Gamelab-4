/*
 * Written by:
 * Henrik
*/

using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(BoxCollider))]
public class LevelHandler : MonoBehaviour
{
	[SerializeField] GameObject explotionParticle;
	[SerializeField] GameObject FlagPrefab;
	[SerializeField] Transform FlagSpawn;

	[SerializeField] StudioEventEmitter _MapErectingSFX;
	[SerializeField] StudioEventEmitter _EjectBoardSFX;

	Transform currentLevel;
	public bool LevelEnding, LevelStarting;
	public bool LevelIsReady => !LevelEnding && !LevelStarting; 
	[SerializeField] float _offsetValue = 0.613f, riseSpeed = 0.05f;

	public ParticleSystem cloudParticle;
	bool cloudsSpawned = false;
	[SerializeField] Transform enemyHolder;

	public ParticleSystem birdParticle;

	void Awake()
	{
		if(!GameObject.FindObjectOfType<FlagPole_Interactable>()) 
			Instantiate(FlagPrefab, FlagSpawn.position, FlagSpawn.rotation, FlagSpawn);	
	}

	// Update is called once per frame
	void Update()
	{
		if(currentLevel)
		{
			if(LevelStarting)
			{
				//PlaySound
				if(!FmodExtensions.IsPlaying(_MapErectingSFX.EventInstance))
                {
					_MapErectingSFX.Play();
                }

				Vector3 pos = currentLevel.localPosition;
				pos.y += Time.deltaTime * riseSpeed;
				currentLevel.localPosition = pos;

				if(currentLevel.localPosition.y > 0)
				{
					// currentLevel.GetComponentInChildren<PathController>().SpawnCubeMeshesAtEvenPoints();
					LevelStarting = false;
					if (cloudsSpawned == false) 
					{ 
						cloudParticle.Play();
						birdParticle.Play();
						cloudsSpawned = true;
					}
				}
			}
			if(LevelEnding)
			{
				Vector3 pos = currentLevel.localPosition;
				pos.y += Time.deltaTime * riseSpeed * 9;
				currentLevel.localPosition = pos;
				if(currentLevel.localPosition.y > 3)
				{
					LevelEnding = false;
					GameObject particle = Instantiate(explotionParticle, currentLevel.transform.position, Quaternion.identity);
					
					particle.transform.GetChild(0).localScale *= 15;
					particle.transform.GetChild(1).localScale *= 15;
					// GameManager.instance.RemoveActiveTowers();

					GameManager.instance.setLights(true);
					Destroy(currentLevel.gameObject);
					currentLevel = null;									
					cloudParticle.Stop();
					birdParticle.Stop();
					cloudsSpawned = false;
				}
			}
		}
	}
	
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		Level_Interactable found = other.GetComponent<Level_Interactable>();
		
		if(found && !currentLevel)
		{
			StartLevel(found.LevelData, found.gameObject);
		}
	}
	
	public void StartLevel(LevelWaveSequence levelData, GameObject board = null)
	{
		if(GameManager.instance)
		{
			GameManager.instance.WaterHighlight.enabled = false;
			CurrencyManager.instance.SetMoney(levelData.StartMoney);
		}
		LevelStarting = true;
		Vector3 offset = Vector3.down * _offsetValue;
		Vector3 newPos = levelData.LevelPrefab.transform.position + offset;
		currentLevel = Instantiate(levelData.LevelPrefab, newPos, Quaternion.identity).transform;
		currentLevel.SetParent(transform, false);
		if(board)
			Destroy(board, 0.1f);
		GameManager.instance.pathController = currentLevel.GetComponentInChildren<PathController>();
	}
	
	public void ExitLevel()
	{
		if(currentLevel)
		{
			//Play Sound
			if (!FmodExtensions.IsPlaying(_EjectBoardSFX.EventInstance))
			{
				_EjectBoardSFX.Play();
			}	  // Unity didn't show FMOD sound. Thus we wait with this to after midterm.

			// Debug.Log(currentLevel.GetComponent<WaveController>());
			currentLevel.GetComponent<WaveController>().EjectParticles.SetActive(true);
			LevelEnding = true;
			CurrencyManager.instance.SetMoney(0);
			// for (int i = 0; i < enemyHolder.childCount; i++)
			// {
			// 	Destroy(enemyHolder.GetChild(i).gameObject);
			// }
			currentLevel.GetComponent<WaveController>().endLevel(true);
		}
	}

	public bool isLevelOnGoing()
	{
		WaveController wave = GetComponentInChildren<WaveController>();
		if(wave)
		{
			if(wave.GetWave == wave.getWaveCount())
				return true;
			return false;
		}
		else
			return true;
	}
}