using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelSpawner : MonoBehaviour
{
	Transform newLevel;
	[SerializeField] float _offsetValue = 0.613f, riseSpeed = 0.05f;
	// Update is called once per frame
	void Update()
	{
		if(newLevel)
		{
			if(newLevel.localPosition.y < 0)
			{
				Vector3 pos = newLevel.localPosition;
				pos.y += Time.deltaTime * riseSpeed;
				newLevel.localPosition = pos;
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
			Vector3 offset = Vector3.down * _offsetValue;
			Vector3 newPos = found.LevelData.LevelPrefab.transform.position + offset;
			newLevel = Instantiate(found.LevelData.LevelPrefab, newPos, Quaternion.identity).transform;
			newLevel.SetParent(transform, false);
			Destroy(found.gameObject, 0.1f);
			GameManager.instance.pathController = newLevel.GetComponentInChildren<PathController>();
		}
	}
}