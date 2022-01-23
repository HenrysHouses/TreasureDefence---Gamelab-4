using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestToCurve : MonoBehaviour
{
	public PathController path;
	
	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos()
	{
		int index = 0;
		Gizmos.color = Color.yellow;
		OrientedPoint point = path.getClosestOP(transform, ref index);		
		Gizmos.DrawSphere(point.pos, 0.1f);
	}
}
