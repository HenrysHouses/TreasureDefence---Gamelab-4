using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class example : MonoBehaviour
{
    public float _Interval;
    float timer;
    float startTime;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 pos = LerpHelper(Vector3.zero, Vector3.forward, startTime, _Interval);
        Debug.Log(pos);
    }

    /// <summary> Smoothly lerps from the Start position to the End position </summary>
    /// <param name="Start">Position of the lerp when interpolation is 0</param>
    /// <param name="End">Position of the lerp when interpolation is 1</param>
    /// <param name="TimeStarted">Static variable Time.time when the lerp began</param>
    /// <param name="Interval">Time between the interpolation goes 0 to 1.</param>
    /// <returns>Vector3 Position between Start and End</returns>
    private Vector3 LerpHelper(Vector3 Start, Vector3 End, float TimeStarted, float Interval = 1)
    {
        //calculates a new lerp location from 0-1 based on how much time has passed since the lerp started
        float TimePassed = Time.time - TimeStarted;
        float lerpLocation = TimePassed / Interval;
    
        //returns new lerped position
        return Vector3.Lerp(Start, End, lerpLocation);
    }
}
