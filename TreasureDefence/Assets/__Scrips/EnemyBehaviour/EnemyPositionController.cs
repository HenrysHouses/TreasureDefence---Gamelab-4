using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPositionController : MonoBehaviour
{
	public PathController path;
	[SerializeField] float tPosition;
	[Range(0.001f, 1)]
	[SerializeField] float speed;

	// Update is called once per frame
	void Update()
	{
		if(tPosition >= 1)
		{
			tPosition = 0;
		}
		tPosition += Time.deltaTime * 0.1f;
		OrientedPoint point = path.GetPathOP(tPosition);
		gameObject.transform.position = point.pos;
		gameObject.transform.rotation = point.rot;
	}
}
