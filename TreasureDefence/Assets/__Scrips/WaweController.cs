using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaweController : MonoBehaviour
{
    public float gameTime;
    public float waweTime; //one wawe lasts the time
    public int currentWawe; 
    public int vaweForLevel; //each map has more wawes
    
    public GameObject winScreen;
    public GameObject loseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waweTime > gameTime)
        {

        }

        if (currentWawe < vaweForLevel)
        {
            winScreen.SetActive(true);
        }
    }
}
