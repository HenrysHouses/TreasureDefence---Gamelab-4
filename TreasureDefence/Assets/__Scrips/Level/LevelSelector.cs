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

    public GameObject[] levelsTesting;

    private GameObject selectedLevel;
    
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
        
    }

    
    
    
    public void SetLevel(int level, string levelName, string levelInfo, int difficulty)
    {
        nameText.text = "" + levelName;
        infoText.text = "" + levelInfo;
        difficultyText.text = "Difficulty: " + difficulty;

        selectedLevel = levelsTesting[level];
        selectedLevel.SetActive(true);
    }

    public void ClearLevel()
    {
        nameText.text = "Location: \n";
        infoText.text = "Lore: \n";
        difficultyText.text = "Difficulty: ";
        
        selectedLevel.SetActive(false);
    }
    
}

