using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTutorial : MonoBehaviour
{
    bool waveStarted = true;
    [SerializeField] GameObject tutorial;

    void Update()
    {
        if(GameManager.instance.GetWaveController())
        {
            if(GameManager.instance.GetWaveController().levelWon && waveStarted)
            {
                tutorial.SetActive(true);
                waveStarted = false;
            }
        }
    }
}
