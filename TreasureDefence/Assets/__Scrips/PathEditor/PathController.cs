using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ExtensionMethods 
{
	public static float Remap (this float from, float fromMin, float fromMax, float toMin,  float toMax)
	{
		var fromAbs  =  from - fromMin;
		var fromMaxAbs = fromMax - fromMin;      
	   
		var normal = fromAbs / fromMaxAbs;
 
		var toMaxAbs = toMax - toMin;
		var toAbs = toMaxAbs * normal;
 
		var to = toAbs + toMin;
	   
		if(to < 0) // inverted to make the path direction correct
	   		to *= -1;
		return to;
	}
	
}

public class PathController : MonoBehaviour
{
	
	[Range(2, 32)]
	[SerializeField] int edgeRingCount = 8;
	
	[Range(0,1)]
	[SerializeField] float tTest = 0;
	// public Transform startPoint;
	// public Transform endPoint;
	[HideInInspector] public Transform startPoint;
	[HideInInspector] public Transform endPoint;
	[HideInInspector] public List<Transform> controlPoints = new List<Transform>();
	Vector3 GetPos(int pair, int i)
	{
		GetStartEndPoints(pair, ref startPoint, ref endPoint);
		
		if(i == 0)
			return startPoint.position;
		if(i == 1)
			return startPoint.TransformPoint(Vector3.forward * startPoint.localScale.z);
		if(i == 2)
			return endPoint.TransformPoint(Vector3.back * endPoint.localScale.z);
		if(i == 3)
			return endPoint.position;
		Debug.LogWarning("GetPos was out of range");
		return default;
	}
	// Vector3 GetPos(int i ) => controlPoints[i].position;

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos()
	{
		// GetApproxLength();
		
		refreshControlPoints();
		
		
		if(controlPoints.Count == 0)
			return;
		foreach(Transform found in controlPoints)
		{
			if(found == null)
				return;
		}	
		for (int j = 0; j < controlPoints.Count-1; j++)
		{
			// Debug.Log(j);
			for(int i = 0; i < 4; i++)
			{
				if(i == 0 || i == 3)
					Gizmos.color = Color.cyan;
				if(i == 1 || i == 2)
					Gizmos.color = Color.red;
				
				float handleSize = 1;
				if(Camera.current != null)
					handleSize = Vector3.Distance(Camera.current.gameObject.transform.position, GetPos(j, i));
				Gizmos.DrawSphere(GetPos(j, i), 0.02f* handleSize);
				Gizmos.DrawLine(GetPos(j, 1), GetPos(j, 0));
				Gizmos.DrawLine(GetPos(j, 2), GetPos(j, 3));
			}	
			
			Handles.DrawBezier(
				GetPos(j, 0), 
				GetPos(j, 3), 
				GetPos(j, 1), 
				GetPos(j, 2), Color.white, EditorGUIUtility.whiteTexture, 1f);
			
			for (int i = 0; i < edgeRingCount+1; i++)
			{
				float t = (1f / edgeRingCount) * i;
				OrientedPoint point = GetBezierOP(j, t);
				

				GetStartEndPoints(j, ref startPoint, ref endPoint);
				Vector3 up = Vector3.Lerp(startPoint.up, endPoint.up, t).normalized;
				
				Handles.DrawLine(point.pos, point.LocalToWorldPos(up));
				
				
				//extracting up vector from quaternion
				// float x = 2f * (point.rot.x*point.rot.y - point.rot.w*point.rot.z);
				// float y = 1f - 2f * (point.rot.x*point.rot.x + point.rot.z*point.rot.z);
				// float z = 2f * (point.rot.y*point.rot.z + point.rot.w*point.rot.x);
				
				// Vector3 up = new Vector3(x,y,z);
				// Vector3 up = point.tangent.normalized;
			}
		} 
		OrientedPoint testPoint = GetPathOP(tTest);
		Handles.PositionHandle(testPoint.pos, testPoint.rot);
	}
	
	OrientedPoint GetBezierOP(int pair, float t)
	{
		Vector3 p0 = GetPos(pair, 0); 
		Vector3 p1 = GetPos(pair, 1); 
		Vector3 p2 = GetPos(pair, 2); 
		Vector3 p3 = GetPos(pair, 3);
		
		Vector3 a = Vector3.Lerp(p0, p1, t); 
		Vector3 b = Vector3.Lerp(p1, p2, t); 
		Vector3 c = Vector3.Lerp(p2, p3, t);
		
		Vector3 d = Vector3.Lerp(a, b, t); 
		Vector3 e = Vector3.Lerp(b, c, t);
		
		Vector3 pos = Vector3.Lerp(d, e , t);
		Vector3 tangent = (e-d).normalized;
		
		Vector3 up = Vector3.Lerp(startPoint.up, endPoint.up, t).normalized;
		Quaternion rot = Quaternion.LookRotation(tangent, up);
		
		return new OrientedPoint(pos, rot);
	}
	
	// ?? somehow an invisible segment at the beginning of t that makes it unresponsive
	public OrientedPoint GetPathOP(float t)
	{
		int selectedSegment = 0;
		float[] segments = new float[controlPoints.Count];
		for (int i = 0; i <= segments.Length-1; i++)
		{
			segments[i] = 1 / ((float)segments.Length-1) * i;
		}
		for (int i = 1; i < segments.Length; i++)
		{
			if(t <= segments[i])
			{
				selectedSegment = i;
				break;
			}
		}
		float tPos;
		if(selectedSegment == segments.Length-1)
			tPos = ExtensionMethods.Remap(t, segments[selectedSegment], segments[selectedSegment-1], 0, 1);
		else
			tPos = ExtensionMethods.Remap(t, segments[selectedSegment], segments[selectedSegment+1], 0, 1);
		tPos = 1-tPos;
		
		selectedSegment = Mathf.Clamp((selectedSegment-1), 0, segments.Length-1);
		OrientedPoint point = GetBezierOP(selectedSegment, tPos);
		return point;
	}
	
	float GetApproxLength(int precision = 8)
	{
		float dist = 0;
		for (int j = 0; j < controlPoints.Count; j++)
		{
			Vector3[] points = new Vector3[precision];
			for (int i = 0; i < precision; i++)
			{
				float t = i / (precision-1);
				points[i] = GetBezierOP(j, t).pos;
			}
			
			for (int i = 0; i < precision-1; i++)
			{
				Vector3 a = points[i];
				Vector3 b = points[i+1];
				dist += Vector3.Distance(a,b);
			}
		}
		return dist;
	}
	
	void GetStartEndPoints(int i, ref Transform start, ref Transform end)
	{
		if(i == 0) // first segment
		{
			start = controlPoints[0];
			end = controlPoints[1];
			return;
		}
		if(i == controlPoints.Count-1) //! bug fix, not sure where this comes from
		{
			return;
		}
		start = controlPoints[i];
		end = controlPoints[i+1];
	}
	
	void refreshControlPoints()
	{
		List<Transform> remove = new List<Transform>();
		for (int i = 0; i < controlPoints.Count; i++)
		{
			if(controlPoints[i] == null)
			{
				remove.Add(controlPoints[i]);
				
				controlPoints.Remove(controlPoints[i]);
				return;
			}
		}
		if(remove.Count != 0)
		{
			foreach(Transform found in remove)
			{
				controlPoints.Remove(found);
			}
		}
		for (int i = 0; i < transform.childCount; i++)
		{
			if(!controlPoints.Contains(transform.GetChild(i)))
			{
				controlPoints.Add(transform.GetChild(i));
			}
		}
		
		if(transform.childCount == 0)
			DestroyImmediate(gameObject);
	}
		
	/// <summary>
	/// Called when the script is loaded or a value is changed in the
	/// inspector (Called in the editor only).
	/// </summary>
	void OnValidate()
	{
		if(controlPoints.Count != transform.childCount)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				if(!controlPoints.Contains(transform.GetChild(i)))
					controlPoints.Add(transform.GetChild(i));
			}
		}
	}
}
