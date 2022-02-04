/*
 * Written by:
 * Henrik
*/

using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelHandler : MonoBehaviour
{
	Transform currentLevel;
	public bool LevelEnded, LevelStarted;
	public bool LevelIsReady => !LevelEnded && !LevelStarted; 
	[SerializeField] float _offsetValue = 0.613f, riseSpeed = 0.05f;
	// Update is called once per frame
	void Update()
	{
		if(currentLevel)
		{
			if(LevelStarted)
			{
				Vector3 pos = currentLevel.localPosition;
				pos.y += Time.deltaTime * riseSpeed;
				currentLevel.localPosition = pos;
				if(currentLevel.localPosition.y > 0)
					LevelStarted = false;
			}
			if(LevelEnded)
			{
				Vector3 pos = currentLevel.localPosition;
				pos.y -= Time.deltaTime * riseSpeed;
				currentLevel.localPosition = pos;
				if(currentLevel.localPosition.y < -_offsetValue)
				{
					LevelEnded = false;
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
		
		if(found)
		{
			StartLevel(found.LevelData, found.gameObject);
		}
	}
	
	public void StartLevel(LevelWaveSequence levelData, GameObject board = null)
	{
		LevelStarted = true;
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
			LevelEnded = true;
		}
	}
}