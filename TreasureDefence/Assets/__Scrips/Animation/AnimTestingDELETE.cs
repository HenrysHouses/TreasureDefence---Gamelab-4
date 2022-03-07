using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTestingDELETE : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CanvasController.instance.OpenCanvas(0,0);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            CanvasController.instance.CloseCanvas();
        }
    }
}
