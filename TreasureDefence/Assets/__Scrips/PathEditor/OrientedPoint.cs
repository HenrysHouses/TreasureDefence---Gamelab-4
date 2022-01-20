using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OrientedPoint
{
	public Vector3 pos;
	public Quaternion rot;
	public Vector3 tangent;
	
	public OrientedPoint(Vector3 pos, Quaternion rot)
	{
		this.pos = pos;
		this.rot = rot;
		this.tangent = new Vector3();
	}
	
	public OrientedPoint(Vector3 pos, Vector3 forward)
	{
		this.pos = pos;
		this.rot = Quaternion.LookRotation(forward);
		this.tangent = new Vector3();
	}
	
	public OrientedPoint(Vector3 pos, Vector3 up, Vector2 tangent)
	{
		this.pos = pos;
		this.rot = Quaternion.LookRotation(tangent, up);
		this.tangent = tangent;
	}
	
	public Vector3 LocalToWorldPos(Vector3 localSpacePos)
	{
		return pos + rot * localSpacePos;
	}
	
	public Vector3 LocalToWorldVect(Vector3 localSpacePos)
	{
		return rot * localSpacePos;
	}
}
