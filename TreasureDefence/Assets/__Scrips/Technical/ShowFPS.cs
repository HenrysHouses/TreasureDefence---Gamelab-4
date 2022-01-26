using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    public Text fpsText;

    public bool show;

    private void Start()
    {
        if (!show)
        {
            fpsText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Show();
        }
        
        fpsText.text = "" + 1 / Time.deltaTime;
    }

    public void Show()
    {
        show = !show;
        
        fpsText.gameObject.SetActive(show);
    }
}
