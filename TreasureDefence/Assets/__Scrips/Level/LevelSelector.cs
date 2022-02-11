/*
 *  Written by
 *  Rune
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector instance;

    private LevelWaveSequence[] levels;

    public GameObject[] levelGameObjects;

    private GameObject selectedLevel;

    public GameObject displayLevel;

    public Transform displayPos;
    
    //[SerializeField] private GameObject displayLevel;
    //[SerializeField] private GameObject displayExtras;
    
    // Level Info
    public TextMeshPro nameText;
    public TextMeshPro infoText;
    public TextMeshPro difficultyText;
    
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        // Will these be in the correct order?
        levels = Resources.LoadAll<LevelWaveSequence>("Levels");
        // Maybe it won't matter whether they are or not?
        
        ClearLevel();

        selectedLevel = new GameObject();
        
        selectedLevel.transform.position = displayPos.position;
        selectedLevel.transform.rotation = displayPos.rotation;
    }

    
    
    
    public void SetLevel(int level, string levelName, string levelInfo, int difficulty)
    {
        nameText.text = "" + levelName;
        infoText.text = "" + levelInfo;
        difficultyText.text = "Difficulty: " + difficulty;

        Destroy(displayLevel);
        displayLevel = Instantiate(levelGameObjects[level-1], displayPos.position, displayPos.rotation);
    }

    public void ClearLevel()
    {
        nameText.text = "Location: \n";
        infoText.text = "Lore: \n";
        difficultyText.text = "Difficulty: ";

        displayLevel = null;

        //selectedLevel.SetActive(false);
    }
}

