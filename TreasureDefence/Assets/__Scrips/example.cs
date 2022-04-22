using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using FMODUnity;

public class example : MonoBehaviour
{
    public string parameterName;
    public int parameterValue;

    bool backgroundImageIsActive;
    Image image;

    StudioParameterTrigger trigger;

    public StudioEventEmitter _audio;


    void Start()
    {
        _audio.Play();
        _audio.SetParameter("name", 1);
        _audio.Stop();
    }

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
