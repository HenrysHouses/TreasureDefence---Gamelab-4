using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPositionController : MonoBehaviour
{
	public PathController path;
	float tPosition;
	[Range(0.001f, 1)]
	[SerializeField] float speed;
	private float speedCorrection = 1;

	// Update is called once per frame
	void Update()
	{
		// Looping for path testing
		if(tPosition >= 1)
		{
			tPosition = 0;
		}		
		
		speedCorrection = 1 - tPosition;
		speedCorrection = Mathf.Clamp(speedCorrection, 0.1f, 1);
		Debug.Log(speedCorrection);
		tPosition += Time.deltaTime * 0.1f * speed * speedCorrection;
		
		
		// Speed test at MAX
		if(speed == 1)
			tPosition *= 1.01f;
		
		
		OrientedPoint point = path.GetPathOP(tPosition);
		gameObject.transform.position = point.pos;
		gameObject.transform.rotation = point.rot;
	}
}
