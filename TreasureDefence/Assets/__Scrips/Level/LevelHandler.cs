/*
 * Written by:
 * Henrik
*/

using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelHandler : MonoBehaviour
{
	Transform currentLevel;
	public bool LevelEnding, LevelStarting;
	public bool LevelIsReady => !LevelEnding && !LevelStarting; 
	[SerializeField] float _offsetValue = 0.613f, riseSpeed = 0.05f;
	// Update is called once per frame
	void Update()
	{
		if(currentLevel)
		{
			if(LevelStarting)
			{
				Vector3 pos = currentLevel.localPosition;
				pos.y += Time.deltaTime * riseSpeed;
				currentLevel.localPosition = pos;
				if(currentLevel.localPosition.y > 0)
				{
					LevelStarting = false;
					// currentLevel.GetComponentInChildren<PathController>().SpawnCubeMeshesAtEvenPoints();

				}
			}
			if(LevelEnding)
			{
				Vector3 pos = currentLevel.localPosition;
				pos.y -= Time.deltaTime * riseSpeed;
				currentLevel.localPosition = pos;
				if(currentLevel.localPosition.y < -_offsetValue)
				{
					LevelEnding = false;
					Destroy(currentLevel.gameObject);
					currentLevel = null;									
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
			LevelEnding = true;
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