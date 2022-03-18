using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    int tutorialScreen = 1;
    [SerializeField] CanvasController canvasController;

    public void nextTutorial()
    {
        if(tutorialScreen+1 > 5)
        {
            tutorialScreen = 1;
            canvasController.CloseCanvas();
            return;
        }
        else
        {
            tutorialScreen = Mathf.Clamp(tutorialScreen+1, 2, 5);
        }

        canvasController.OpenNewCanvas(tutorialScreen, 1);
    }
}
