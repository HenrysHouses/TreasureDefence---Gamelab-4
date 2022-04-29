using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using FMODUnity;

public class example : MonoBehaviour
{
    public float range;
    public List<GameObject> unsorted = new List<GameObject>();
    public List<GameObject> NewSorted = new List<GameObject>();
   
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Update()
    {
        NewSorted = sortList();
    }

    List<GameObject> sortList()
    {
        List<GameObject> Targets = inRange();
        List<GameObject> sorted = new List<GameObject>();
        for (int i = 0; i < Targets.Count; i++)
        {
            int closestTargetIndex = 0;
            float closestDist = Mathf.Infinity;
            for (int j = 0; j < Targets.Count; j++)
            {
                float checkDist = Vector3.Distance(transform.position, Targets[j].transform.position);

                Debug.Log("i: "+ i + ", j: " + j);
                if (checkDist < closestDist && !sorted.Contains(Targets[j]))
                {
                    closestTargetIndex = j;
                    closestDist = Vector3.Distance(transform.position, Targets[closestTargetIndex].transform.position);
                }
            }
            Debug.Log(closestTargetIndex + " : " + closestDist);
            if(!sorted.Contains(Targets[closestTargetIndex]))
            {
                sorted.Add(Targets[closestTargetIndex]);
            }
        }
        return sorted;
    }

    List<GameObject> inRange()
    {
        List<GameObject> enemiesInRange = new List<GameObject>();
		for (int i = 0; i < unsorted.Count; i++)
		{
			float dist = Vector3.Distance(transform.position, unsorted[i].transform.position);
	
			if (dist < range)
			{
				enemiesInRange.Add(unsorted[i]);
			}
		}
		return enemiesInRange;
    }
}
