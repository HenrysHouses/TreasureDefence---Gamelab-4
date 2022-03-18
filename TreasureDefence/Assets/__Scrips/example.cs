using UnityEngine;
using System.Collections.Generic;
using FMODUnity;

public class example : MonoBehaviour
{
    public StudioEventEmitter _audio;
    public string parameterName;
    public int parameterValue;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            _audio.Play();
            _audio.SetParameter(parameterName, parameterValue);
            Debug.Log(parameterName + ": " + parameterValue);
            
        }
    }
}
