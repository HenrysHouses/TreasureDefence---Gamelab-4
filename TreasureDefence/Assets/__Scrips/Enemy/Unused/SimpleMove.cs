using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
	public float speed;
	
	void Update()
	{
		Debug.LogWarning("this script is deprecated");
		
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
