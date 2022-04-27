using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using FMODUnity;

public class example : MonoBehaviour
{
    public List<GameObject> unsorted = new List<GameObject>();
    public List<GameObject> sorted = new List<GameObject>();
    void Start()
    {
    }

    void Update()
    {
        sortList();
    }

    void sortList()
    {
        for (int i = 0; i < unsorted.Count; i++)
        {
            int closestTargetIndex = 0;
            float closestDist = Mathf.Infinity;
            for (int j = 0; j < unsorted.Count; j++)
            {
                float checkDist = Vector3.Distance(transform.position, unsorted[j].transform.position);

                Debug.Log("i: "+ i + ", j: " + j);
                if (checkDist < closestDist && !sorted.Contains(unsorted[j]))
                {
                    closestTargetIndex = j;
                    closestDist = Vector3.Distance(transform.position, unsorted[closestTargetIndex].transform.position);
                }
            }
            Debug.Log(closestTargetIndex + " : " + closestDist);
            if(!sorted.Contains(unsorted[closestTargetIndex]))
            {
                sorted.Add(unsorted[closestTargetIndex]);
            }
        }
    }
}
