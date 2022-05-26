using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PirateCoveController : MonoBehaviour
{
    public static PirateCoveController instance;
    
    private int _experience;

    public TextMeshPro experienceText;

    public TextMeshPro[] costs;

    public GameObject[] checkSigns;
    public GameObject[] crossSigns;

    public GameObject[] upgradeIcons;
    
    private float[] mult = { 1f, 1.08f, 1.32f, 1.25f, 1.42f };

    private int[] buildingsLevels = {0,0,0,0,0};
    
    /*                                                                      Descriptions
     ________________________________________________________________________________________________________________________________ 
    |                                                                                                                               |
    |    Wall            Decides pirate cove HP                            Stronger walls will give us an advantage over the brits. |
    |    Chest           Increase money gained from enemies                Bigger chest, bigger coins! Harrharr                     |
    |    Tavern          Increase tower range                              Grog is pirate focus-juice!                              |
    |    Blacksmith      Increase tower damage                             Weapons created for maximum destruction and power.       |
    |    Barracks        Increase tower attack speed                       A rested pirate is a quick pirate!                       |
    |    Guard Tower     Spot for permanent tower that can be upgraded     A custom tower specialized for ship destruction          |
    |_______________________________________________________________________________________________________________________________| 

    General Upgrade Effects
    Level 0 : 100%
    Level 1 : 110%
    Level 2 : 120%
    Level 3 : 130%
    Level 4 : 140%
    Level 5 : 150%

    Wall Upgrade Effects
    Level 0 : 100%
    Level 1 : 125%
    Level 2 : 150%
    Level 3 : 175%
    Level 4 : 200%
    Level 5 : 250%

    Guard Tower Upgrade Effects
    Level 0 : No tower
    Level 1 : Low Damage, Low Attack Speed, Low Range, Max Targets: 1
    Level 2 : Low Damage, Med Attack Speed, Low Range, Max Targets: 1
    Level 3 : Med Damage, Med Attack Speed, Low Range, Max Targets: 2
    Level 4 : Med Damage, Max Attack Speed, Med Range, Max Targets: 2
    Level 5 : Med Damage, Max Attack Speed, Max Range, Max Targets: 3

    */

    /*
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject tavern;
    [SerializeField] private GameObject blacksmith;
    [SerializeField] private GameObject barracks;
    [SerializeField] private GameObject guardTower;
    */

    [SerializeField] private GameObject[] wallModels;
    [SerializeField] private GameObject[] chestModels;
    [SerializeField] private GameObject[] tavernModels;
    [SerializeField] private GameObject[] blacksmithModels;
    [SerializeField] private GameObject[] barracksModels;
    //[SerializeField] private GameObject[] guardTowerModels;

    private float[] wallUpgrades = { 1f, 1.25f, 1.5f,  1.9f, 2.4f, 3f };

    private float[] towerUpgrades = { 1f, 1.1f, 1.23f,  1.44f, 1.67f, 2f };
    
    private float[] rangeUpgrades = { 1f, 1.05f, 1.12f,  1.2f, 1.3f, 1.45f };

    private float[] chestUpgrades = { 1f, 1.1f, 1.23f,  1.44f, 1.67f, 2f };


    // Upgrades
    /*
    private int _wallLevel;
    private int _chestLevel;
    private int _tavernLevel;
    private int _blacksmithLevel;
    private int _barracksLevel;
    */
    //private int _guardTowerLevel;

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

<<<<<<< Updated upstream
            // Debug.Log("Level " + temp + " : " + CalculateCost(i) + " xp.");
=======
        for (int i = 0; i < costs.Length; i++)
        {
            costs[i].text = "" + CalculateCost(0, mult[i]);
            checkSigns[i].SetActive(false);
>>>>>>> Stashed changes
        }

        experienceText.text = "" + _experience;

    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                AddExperience(1);
                Debug.Log("Experience: " + _experience);
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Upgrade(0);
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Upgrade(1);
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Upgrade(2);
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Upgrade(3);
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Upgrade(4);
            }
        }
    }

    public void CalculateAll()
    {
        /*
        for (int i = 0; i < checkSigns.Length; i++)
        {
            if (_experience > CalculateCost(i))
            {
                checkSigns[i].SetActive(true);
                crossSigns[i].SetActive(false);
            }
            else
            {
                checkSigns[i].SetActive(false);
                crossSigns[i].SetActive(true);
            }
        }
        */

        for (int i = 0; i < buildingsLevels.Length; i++)
        {
            if (_experience >= CalculateCost(buildingsLevels[i], mult[i]))
            {
                upgradeIcons[i].SetActive(true);
                checkSigns[i].SetActive(true);
                crossSigns[i].SetActive(false);
            }
            else
            {
                upgradeIcons[i].SetActive(false);
                checkSigns[i].SetActive(false);
                crossSigns[i].SetActive(true);
            }
        }
        
        /*
        if (_experience >= CalculateCost(_wallLevel, mult[0]))
        {
            upgradeIcons[0].SetActive(true);
            checkSigns[0].SetActive(true);
            crossSigns[0].SetActive(false);
        }
        else
        {
            upgradeIcons[0].SetActive(false);
            checkSigns[0].SetActive(false);
            crossSigns[0].SetActive(true);
        }
        
        if (_experience >= CalculateCost(_chestLevel, mult[1]))
        {
            upgradeIcons[1].SetActive(true);
            checkSigns[1].SetActive(true);
            crossSigns[1].SetActive(false);
        }
        else
        {
            upgradeIcons[1].SetActive(false);
            checkSigns[1].SetActive(false);
            crossSigns[1].SetActive(true);
        }
        
        if (_experience >= CalculateCost(_tavernLevel, mult[2]))
        {
            upgradeIcons[2].SetActive(true);
            checkSigns[2].SetActive(true);
            crossSigns[2].SetActive(false);
        }
        else
        {
            upgradeIcons[2].SetActive(false);
            checkSigns[2].SetActive(false);
            crossSigns[2].SetActive(true);
        }
        
        if (_experience >= CalculateCost(_blacksmithLevel, mult[3]))
        {
            upgradeIcons[3].SetActive(true);
            checkSigns[3].SetActive(true);
            crossSigns[3].SetActive(false);
        }
        else
        {
            upgradeIcons[3].SetActive(false);
            checkSigns[3].SetActive(false);
            crossSigns[3].SetActive(true);
        }
        
        if (_experience >= CalculateCost(_barracksLevel, mult[4]))
        {
            upgradeIcons[4].SetActive(true);
            checkSigns[4].SetActive(true);
            crossSigns[4].SetActive(false);
        }
        else
        {
            upgradeIcons[4].SetActive(false);
            checkSigns[4].SetActive(false);
            crossSigns[4].SetActive(true);
        }
        */
        /*
        if (_experience > CalculateCost(0))
            checkSigns[0].SetActive(true);
        else
            checkSigns[0].SetActive(false);

        if (_experience > CalculateCost(1))
            checkSigns[1].SetActive(true);
        else
            checkSigns[1].SetActive(false);
        
        if (_experience > CalculateCost(2))
            checkSigns[2].SetActive(true);
        else
            checkSigns[2].SetActive(false);
        
        if (_experience > CalculateCost(3))
            checkSigns[3].SetActive(true);
        else
            checkSigns[3].SetActive(false);
        
        if (_experience > CalculateCost(4))
            checkSigns[4].SetActive(true);
        else
            checkSigns[4].SetActive(false);
        */
    }

    public int CalculateCost(int structureLevel, float multiplier)
    {
        int val = (structureLevel + 1) * 5;

        return (Mathf.FloorToInt(Mathf.Pow(val, 2.22f) * multiplier) + 1);
    }

    public void Upgrade(int structure)
    {
        switch (structure)
        {
            case 0:
                if (RemoveExperience(CalculateCost(buildingsLevels[0], mult[0])))
                {
                    //wallModels[_wallLevel].SetActive(false);
                    
                    buildingsLevels[0] += 1;

                    wallModels[buildingsLevels[0]].SetActive(true);
                    GameManager.instance.healthMultiplier = wallUpgrades[buildingsLevels[0]];

                    costs[0].text = "" + CalculateCost(buildingsLevels[0], mult[0]);
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + buildingsLevels[0] + 1);

                break;

            case 1:
                if (RemoveExperience(CalculateCost(buildingsLevels[1], mult[1])))
                {
                    //chestModels[_chestLevel].SetActive(false);
                    
                    buildingsLevels[1] += 1;

                    chestModels[buildingsLevels[1]].SetActive(true);
                    GameManager.instance.moneyMultiplier = chestUpgrades[buildingsLevels[1]];
                    
                    costs[1].text = "" + CalculateCost(buildingsLevels[1], mult[1]);
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + buildingsLevels[1] + 1);

                break;

            case 2:
                if (RemoveExperience(CalculateCost(buildingsLevels[2], mult[2])))
                {
                    //tavernModels[_tavernLevel].SetActive(false);
                    
                    buildingsLevels[2] += 1;

                    tavernModels[buildingsLevels[2]].SetActive(true);
                    GameManager.instance.rangeMultiplier = rangeUpgrades[buildingsLevels[2]];
                    
                    costs[2].text = "" + CalculateCost(buildingsLevels[2], mult[2]);
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + buildingsLevels[2] + 1);

                break;

            case 3:
                if (RemoveExperience(CalculateCost(buildingsLevels[3], mult[3])))
                {
                    //blacksmithModels[_blacksmithLevel].SetActive(false);
                    
                    buildingsLevels[3] += 1;
                    
                    blacksmithModels[buildingsLevels[3]].SetActive(true);
                    GameManager.instance.damageMultiplier = towerUpgrades[buildingsLevels[3]];
                    
                    costs[3].text = "" + CalculateCost(buildingsLevels[3], mult[3]);
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + buildingsLevels[3] + 1);

                break;

            case 4:
                if (RemoveExperience(CalculateCost(buildingsLevels[4], mult[4])))
                {
                    //barracksModels[_barracksLevel].SetActive(false);
                    
                    buildingsLevels[4] += 1;

                    barracksModels[buildingsLevels[4]].SetActive(true);
                    GameManager.instance.attSpeedMultiplier = 1f / towerUpgrades[buildingsLevels[4]];
                    
                    costs[4].text = "" + CalculateCost(buildingsLevels[4], mult[4]);
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + buildingsLevels[4] + 1);
                break;
        }
    }

    private void ValueChanged()
    {
        experienceText.text = "" + _experience;
        
        CalculateAll();
    }

    public void AddExperience(int num = 1)
    {
        _experience += num;
        
        ValueChanged();
    }

    public bool RemoveExperience(int num)
    {
        if (_experience - num < 0) return false;

        _experience -= num;
        
        ValueChanged();
        
        return true;
    }

    public int GetWallLevel()
    {
        return buildingsLevels[0];
    }

    public int GetChestLevel()
    {
        return buildingsLevels[1];
    }

    public int GetTavernLevel()
    {
        return buildingsLevels[2];
    }

    public int GetBlacksmithLevel()
    {
        return buildingsLevels[3];
    }

    public int GetBarracksLevel()
    {
        return buildingsLevels[4];
    }
}