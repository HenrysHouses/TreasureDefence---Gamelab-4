using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using FMODUnity;

public class example : MonoBehaviour
{
    public StudioEventEmitter _audio;
    public string parameterName;
    public int parameterValue;

    bool backgroundImageIsActive;
    Image image;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
           backgroundImageIsActive = !backgroundImageIsActive;
        }

        if(image.enabled != backgroundImageIsActive)
        {
            image.enabled = backgroundImageIsActive;
        }
    }
}
