using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Test_Interactable : TD_Interactable
{
    public override void InteractTrigger(object target = null)
    {
        string method = target as string;

        if(method == "Grip")
            GetComponent<MeshRenderer>().material.color = Color.green;
        if(method == "FingerGun")
            GetComponent<MeshRenderer>().material.color = Color.yellow;
        if(method == "Point")
            GetComponent<MeshRenderer>().material.color = Color.black;
        if(method == "ThumbsUp")
            GetComponent<MeshRenderer>().material.color = Color.blue;
        if(method == "Fist")
            GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log("VR trigger interaction");
    }

    public override void InteractionEndTrigger(object target = null)
    {
        Debug.Log("VR END trigger interaction");
        GetComponent<MeshRenderer>().material.color = Color.white;

    }
}
