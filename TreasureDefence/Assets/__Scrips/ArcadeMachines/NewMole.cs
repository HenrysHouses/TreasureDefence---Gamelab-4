using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class NewMole : MonoBehaviour
{
    //public Transform hidePos, showPos;

    Vector3 moleOrigin, showPos;
    public float showOffset;
    [SerializeField]private float moleSpeed, timePassed;


    void Awake()
    {
        moleOrigin = transform.position;
        Debug.Log(moleOrigin);
        showPos = transform.position;
        showPos.y += showOffset;

        Debug.Log(showPos);
    }
    // Update is called once per frame
    void Update()
    {
        MoveMole();

    }
    void MoveMole()
    {
        timePassed += Time.deltaTime;
        gameObject.transform.position = Vector3.Lerp(showPos, moleOrigin, Mathf.PingPong(timePassed * (moleSpeed / 10), 1));
        //Debug.Log("Lerping");
    }
}
