using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example2 : MonoBehaviour
{
    public Transform moveTo;
    public List<GameObject> moveThese = new List<GameObject>();
    float time;
    public float offset = 1,  speed = 1;

    void Update()
    {
        time += Time.deltaTime * speed;
        for (int i = 0; i < moveThese.Count; i++)
        {
            float t =  time - offset * i;
            moveThese[i].transform.position = Vector3.Lerp(transform.position, moveTo.position, t%1);
        }
    }
}
