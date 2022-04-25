using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class HighlightSound_Mikkel : MonoBehaviour
{
    public Renderer HighlightRenderer;
    [SerializeField] StudioEventEmitter HighlightSFX;

    private void Start()
    {
        //HighlightRenderer.enabled = false;
    }

    void Update()
    {

          if(HighlightRenderer.enabled == true)
           {
            
            if (!FmodExtensions.IsPlaying(HighlightSFX.EventInstance))
            {
                HighlightSFX.Play();
                //HighlightSFX.SetParameter("Valid_Invalid", 0);       //This has no parameter.
            }
            
           }

        if (HighlightRenderer.enabled == false)
        {
                HighlightSFX.Stop();
        }

    }
}
