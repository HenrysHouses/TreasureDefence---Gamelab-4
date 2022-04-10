using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    [SerializeField] Tutorial_Highlighter tut;
    int tutorialScreen = 1;
    [SerializeField] CanvasController canvasController;

    public void nextTutorial()
    {
        if(tut)
            tut.setTutorialText("Press me " + tutorialScreen + "/5");

        if(tutorialScreen+1 > 5)
        {
            tutorialScreen = 1;
            canvasController.CloseCanvas();
            tut.gameObject.SetActive(false);
            return;
        }
        else
        {
            tutorialScreen = Mathf.Clamp(tutorialScreen+1, 2, 5);
        }

        canvasController.OpenNewCanvas(tutorialScreen);
    }
}
