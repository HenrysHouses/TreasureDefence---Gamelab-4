using UnityEngine;
using System.Collections.Generic;

public class example : MonoBehaviour
{
    public GameObject prefab;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }







    // bool shouldLerp, lerpHasStarted;
    // public float _Interval;
    // float startTime;
    // void Start()
    // {
    //     startTime = Time.time;
    //     Debug.Log(recursivePrint(10));
    // }

    // public float speed;
    // public float num;

	// public string recursivePrint(int ammount, int currentCount = 0)
	// {
	// 	string stars = "";
	// 	int count = 0;

	// 	for(int i = 0; i < ammount - count; i++)
	// 	{
	// 		stars += "*";
	// 	}
	// 	stars += "\n";
	// 	count++;
	// 	if(count != ammount)
	// 		return stars + recursivePrint(ammount, count);
	// 	return "";
	// }


    // // Update is called once per frame
    // void Update()
    // {


    //     num += speed * Time.deltaTime;
    //     Debug.Log(PingPongExtention(num, 2,5, 4));
























        // if(shouldLerp)
        // {
        //     if(!lerpHasStarted)
        //     {
        //         lerpHasStarted = true;
        //         startTime = Time.time;
        //     }

        //     // Vector3 pos = LerpHelper(Vector3.zero, Vector3.forward, startTime, _Interval);
        //     Debug.Log(pos);

        //     if(pos == Vector3.forward)
        //     {
        //         shouldLerp = false;
        //         lerpHasStarted = false;
        //     }
        // }
    // }

    // / <summary> Smoothly lerps from the Start position to the End position </summary>
    // / <param name="Start">Position of the lerp when interpolation is 0</param>
    // / <param name="End">Position of the lerp when interpolation is 1</param>
    // / <param name="TimeStarted">Static variable Time.time when the lerp began</param>
    // / <param name="Interval">Time between the interpolation goes 0 to 1.</param>
    // / <returns>Vector3 Position between Start and End</returns>
    // private Vector3 LerpHelper(Vector3 Start, Vector3 End, float TimeStarted, float Interval = 1)
    // {
    //     //calculates a new lerp location from 0-1 based on how much time has passed since the lerp started
    //     float TimePassed = Time.time - TimeStarted;
    //     float lerpLocation = TimePassed / Interval;
    
    //     //returns new lerped position
    //     return Vector3.Lerp(Start, End, lerpLocation);
    // }


    // public float PingPongExtention(float t, float rangeFrom, float rangeTo, float offset)
    // {
    //     float remapped = 0;
    //     if(t % rangeTo > offset)
    //         remapped = ExtensionMethods.Remap( t % rangeTo, rangeFrom, rangeTo, rangeTo, rangeFrom); // 1->0
    //     else
    //         remapped = t % rangeTo;
    //     return remapped;
    // }
}
