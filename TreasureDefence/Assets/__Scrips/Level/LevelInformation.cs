using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInformation : MonoBehaviour
{
    public static LevelInformation instance;

    public string levelName;
    public string levelInformation;
    public int difficulty;

    public Text nameText;
    public Text infoText;
    public Text difficultyText;
    
    
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
    }
    

    public void SetText(string name, string levelInfo, int difficulty)
    {
        nameText.text = "Location: \n" + name;
        infoText.text = "Lore: \n" + levelInfo;
        difficultyText.text = "Difficulty: " + difficulty;
    }

    public void ClearText()
    {
        nameText.text = "Location: \n";
        infoText.text = "Lore: \n";
        difficultyText.text = "Difficulty: ";
    }
}
