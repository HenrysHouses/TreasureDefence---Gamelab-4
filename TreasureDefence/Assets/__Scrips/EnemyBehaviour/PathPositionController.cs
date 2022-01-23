using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPositionController : MonoBehaviour
{
	public PathController path;
	float pathDist = 0f;
	[Range(0.001f, 1)]
	[SerializeField] float speed;
	private float speedCorrection = 1;
	float offset;

	// Update is called once per frame
	
	
	float posDelta, lastTPos;
	
	float tPos;
	
	void Start()
	{
		offset = path.GetEvenPathTOffset(6);
		pathDist = offset;
	}
	
	void Update()
	{
		
		// Looping for path testing
		if(pathDist >= 1)
		{
			pathDist = offset;
		}		
		
		// Speed test at MAX
		// if(speed == 1)
		// 	pathDist *= 1.01f;
		
		
		
		pathDist += Time.deltaTime * speed;
		
		posDelta = pathDist - lastTPos;
		lastTPos = pathDist;
		
		OrientedPoint point = path.GetEvenPathOP(pathDist);
		gameObject.transform.position = point.pos;
		gameObject.transform.rotation = point.rot;
	}
}
