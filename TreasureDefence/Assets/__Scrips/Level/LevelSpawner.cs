using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelSpawner : MonoBehaviour
{
	// Update is called once per frame
	void Update()
	{
		
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
			Debug.Log("found");
		}
	}
}