using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( CurveExtender ) )]
public class CurveEditor : Editor
{
	private PathController newSegment;
	void OnSceneGUI()
	{
		CurveExtender t = target as CurveExtender;
		
		// if( t == null)
		// 	return;

		Event e = Event.current;
		
		Camera cam = Camera.current;
		Vector3 pos = Event.current.mousePosition;
		if(cam != null)
		{
			pos.z = -cam.worldToCameraMatrix.MultiplyPoint(t.transform.position).z;
			pos.y = Screen.height - pos.y - 36.0f; // ??? Why that offset?!
			pos = cam.ScreenToWorldPoint (pos);
		}
		// if(Event.current.ToString() != "repaint" && Event.current.ToString() != "Layout" )
		// 	Debug.Log(Event.current);
		if(e.type == EventType.MouseDown && e.ToString().Contains("Modifiers: Control"))
		{
			if(t.GetComponent<PathController>() == null)
			{
				newSegment = t.transform.parent.gameObject.GetComponent<PathController>();
				GameObject newPoint = new GameObject();
				newPoint.transform.SetParent(t.gameObject.transform.parent);
				newPoint.transform.position = pos;
				int n = newSegment.controlPoints.Count;
				newPoint.name = "p"+n;
				newPoint.AddComponent<CurveExtender>();
				newSegment.controlPoints.Add(newPoint.transform);
				
				// newSegment.startPoint = t.gameObject.transform;
				// newSegment.endPoint = new GameObject().transform;
				// newSegment.endPoint.SetParent(t.gameObject.transform.parent);
				// newSegment.endPoint.position = pos;
				// int n = int.Parse(t.gameObject.name.Trim('p'))+1;
				// newSegment.endPoint.name = "p"+n;
				// Selection.SetActiveObjectWithContext(newSegment.endPoint);
				// Selection.objects = new Object[] { newSegment.endPoint };
			}
		}
	}
}