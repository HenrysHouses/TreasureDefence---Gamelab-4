using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPositionController : MonoBehaviour
{
	public PathController path;
	float pathDist = 0f;
	float tPos;
	[Range(0.001f, 1)]
	[SerializeField] float speed;
	private float speedCorrection = 1;

	// Update is called once per frame
	
	
	float posDelta, lastTPos;
	
	
	void Update()
	{
		
		// Looping for path testing
		if(pathDist >= path.length)
		{
			pathDist = 0f;
		}		
		
		
		
		/* 
		!  Deprecated t pos approximation
		// Debug.Log(path.GetApproxLength());		
		speedCorrection = 1^(10+(int)tPosition);
		// Debug.Log(speedCorrection);
		tPosition += Time.deltaTime * 0.1f * speed * speedCorrection;
		// Debug.Log("correction: " + speedCorrection + " t: " + tPosition);
		*/
		
		// Speed test at MAX
		// if(speed == 1)
		// 	pathDist *= 1.01f;
		
		
		
		pathDist += Time.deltaTime * speed;
		// tPos = path.DistToT(path.LUT, pathDist);
		
		posDelta = tPos - lastTPos;
		lastTPos = tPos;
		
		OrientedPoint point = path.GetPathOP(tPos);
		gameObject.transform.position = point.pos;
		gameObject.transform.rotation = point.rot;
	}
}
