using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helpbutton_Tutorial : MonoBehaviour
{
    int slide = 0;

    [SerializeField] Tutorial_Highlighter tutorial;
    [SerializeField] int AmountOfSlides = 5;
    bool hasProgressed;
    [SerializeField] GameObject nextTutorial, _thisTutorial;

    void Start()
    {
        tutorial.setTutorialText("Grab me! " + slide + "/" + AmountOfSlides);
    }

    public void progressTutorial()
    {
        slide++;
        tutorial.setTutorialText("Grab me! " + slide + "/" + AmountOfSlides);
        if(slide >= AmountOfSlides && !hasProgressed)
        {
            hasProgressed = true;
            nextTutorial.SetActive(true);
            _thisTutorial.SetActive(false);
        }
    }
}
