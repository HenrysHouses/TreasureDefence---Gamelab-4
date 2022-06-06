using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;
    
    public bool open;
    public Animator graphicsAnimator;
    public GameObject[] graphics;
    [SerializeField] private int currentGraphicsNumber;
    public int getCurrentGaphic => currentGraphicsNumber;

    // 0, 1, 2 / front, mid, back
    //public Animator[] canvasAnimators;  // Have only one canvas?
    public Animator canvasAnimator;

    [SerializeField] StudioEventEmitter CanvasSFX;
    

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void OpenNewCanvas(int graphic = -1)
    {
        StartCoroutine(OpenCanvasHelper(0, graphic, 1));
    }

    public IEnumerator OpenCanvasHelper(int canvas = -1, int graphic = -1, float timeToClose = -1)
    {
        if(open)
            CloseCanvas();
        if(timeToClose > 0)
            yield return new WaitForSeconds(timeToClose);
        
        OpenCanvas(canvas, graphic, timeToClose);
    }


    public void OpenCanvas(int canvas = -1, int graphic = -1, float timeToClose = -1)
    {
        if (!open)
        {
            if (canvas == -1 || graphic == -1 || timeToClose < 0)
                return;

            graphics[currentGraphicsNumber].SetActive(false);
        
            currentGraphicsNumber = graphic;
            graphics[graphic].SetActive(true);
            graphicsAnimator.Play("Open");
        
            canvasAnimator.Play("OpenCanvas");
            CanvasSFX.Play();
            CanvasSFX.SetParameter("Valid_Invalid", 0);
            

            //canvasesOpen[canvas] = true;

            open = true;
            
            // Invoke("CloseCanvas", timeToClose);
            
        }
        else
            Debug.Log("the cavas was already open");

    }

    public void CloseCanvas()
    {
        canvasAnimator.Play("CloseCanvas");
        
                
        graphicsAnimator.Play("Close");
        
        foreach (var ghapic in graphics)
        {
            ghapic.SetActive(false);
        }
        CanvasSFX.Play();
        CanvasSFX.SetParameter("Valid_Invalid", 1);

        /*
        for (int i = 0; i < canvasesOpen.Length; i++)
        {
            if (canvasesOpen[i])
            {
                canvasAnimator.Play("CloseCanvas");
                graphicsAnimator.Play("Close");
            
                //canvasesOpen[i] = false;  
            }
        }
        */
        Invoke("ResetCanvas", 1f);
    }

    private void ResetCanvas()
    {
        open = false;
    }

}
