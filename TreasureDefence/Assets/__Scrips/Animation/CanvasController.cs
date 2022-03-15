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
    private int currentGraphicsNumber;

    // 0, 1, 2 / front, mid, back
    //public Animator[] canvasAnimators;  // Have only one canvas?
    public Animator canvasAnimator;
    
    private bool[] canvasesOpen;


    

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
            //canvasesOpen[canvas] = true;

            open = true;
            
            // Invoke("CloseCanvas", timeToClose);
            
        }

    }

    public void CloseCanvas()
    {
        canvasAnimator.Play("CloseCanvas");
        
                
        graphicsAnimator.Play("Close");
        
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
