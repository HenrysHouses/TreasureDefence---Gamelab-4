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

    private int[] buildingLevels = { 0, 0, 0, 0, 0 };

    private bool[] buildingMaxed = { false, false, false, false, false };

    [SerializeField] private ParticleSystem[] wallParticles;
    [SerializeField] private ParticleSystem[] chestParticles;
    [SerializeField] private ParticleSystem[] tavernParticles;
    [SerializeField] private ParticleSystem[] smithyParticles;
    [SerializeField] private ParticleSystem[] barracksParticles;
    
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

        for (int i = 0; i < costs.Length; i++)
        {
            costs[i].text = "" + CalculateCost(0, mult[i]);
            checkSigns[i].SetActive(false);
            upgradeIcons[i].SetActive(false);
        }

        experienceText.text = "" + _experience;

    }

    private void Update()
    {
        if (Application.isEditor)       // REMOVE BEFORE FINAL HAND IN
        {
            if (Input.GetKey(KeyCode.Space))
            {
                AddExperience(10);
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

        for (int i = 0; i < buildingLevels.Length; i++)
        {
            if (_experience >= CalculateCost(buildingLevels[i], mult[i]))
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
        if (structureLevel > 4)
            return 10000000;
        
        int val = (structureLevel + 1) * 5;
        
        return (Mathf.FloorToInt(Mathf.Pow(val, 2.22f) * multiplier) + 1);
    }

    public void Upgrade(int structure)
    {
        switch(structure)
        {
            case 0:     // WALL        WALL        WALL        WALL
                if (!buildingMaxed[structure])
                {
                    if (RemoveExperience(CalculateCost(buildingLevels[structure], mult[structure])))
                    {
                        wallModels[buildingLevels[structure]].SetActive(true);
                        
                        buildingLevels[structure] += 1;

                        GameManager.instance.healthMultiplier = wallUpgrades[buildingLevels[structure]];

                        for (int i = 0; i < wallParticles.Length; i++)
                        {
                            wallParticles[i].Play();
                        }

                        if (buildingLevels[structure] <= 4)
                            costs[structure].text = "" + CalculateCost(buildingLevels[structure], mult[structure]);
                        else
                        {
                            costs[structure].text = "MAX";
                            buildingMaxed[structure] = true;
                        }
                    }
                    else
                        Debug.Log("Insufficient _experience points to purchase Wall Level " + buildingLevels[structure] + 1);
                }
                break;

            case 1:     // CHEST        CHEST       CHEST       CHEST
                if (!buildingMaxed[structure])
                {
                    if (RemoveExperience(CalculateCost(buildingLevels[structure], mult[structure])))
                    {
                        chestModels[buildingLevels[structure]].SetActive(true);

                        buildingLevels[structure] += 1;
                        
                        GameManager.instance.moneyMultiplier = chestUpgrades[buildingLevels[structure]];
                        
                        for (int i = 0; i < chestParticles.Length; i++)
                        {
                            chestParticles[i].Play();
                        }

                        if (buildingLevels[structure] <= 4)
                            costs[structure].text = "" + CalculateCost(buildingLevels[structure], mult[structure]);
                        else
                        {
                            costs[structure].text = "MAX";
                            buildingMaxed[structure] = true;
                        }
                    }
                    else
                        Debug.Log("Insufficient _experience points to purchase Chest Level " + buildingLevels[structure] + 1);
                }
                break;

            case 2:     // TAVERN       TAVERN      TAVERN      TAVERN
                if (!buildingMaxed[structure])
                {
                    if (RemoveExperience(CalculateCost(buildingLevels[structure], mult[structure])))
                    {
                        tavernModels[buildingLevels[structure]].SetActive(true);

                        buildingLevels[structure] += 1;
                        
                        GameManager.instance.rangeMultiplier = rangeUpgrades[buildingLevels[structure]];
                        
                        for (int i = 0; i < tavernParticles.Length; i++)
                        {
                            tavernParticles[i].Play();
                        }

                        if (buildingLevels[structure] <= 4)
                            costs[structure].text = "" + CalculateCost(buildingLevels[structure], mult[structure]);
                        else
                        {
                            costs[structure].text = "MAX";
                            buildingMaxed[structure] = true;
                        }
                    }
                    else
                        Debug.Log("Insufficient _experience points to purchase Tavern Level " + buildingLevels[structure] + 1);
                }
                break;

            case 3:     // SMITH        SMITH       SMITH       SMITH
                if (!buildingMaxed[structure])
                {
                    if (RemoveExperience(CalculateCost(buildingLevels[structure], mult[structure])))
                    {
                        blacksmithModels[buildingLevels[structure]].SetActive(true);
                        buildingLevels[structure] += 1;
                        
                        GameManager.instance.damageMultiplier = towerUpgrades[buildingLevels[structure]];
                        
                        for (int i = 0; i < smithyParticles.Length; i++)
                        {
                            smithyParticles[i].Play();
                        }

                        if (buildingLevels[structure] <= 4)
                            costs[structure].text = "" + CalculateCost(buildingLevels[structure], mult[structure]);
                        else
                        {
                            costs[structure].text = "MAX";
                            buildingMaxed[structure] = true;
                        }
                    }
                    else
                        Debug.Log("Insufficient _experience points to purchase Blacksmith Level " + buildingLevels[structure] + 1);
                }
                break;

            case 4:     // BARRACKS     BARRACKS        BARRACKS        BARRACKS
                if (!buildingMaxed[structure])
                {
                    if (RemoveExperience(CalculateCost(buildingLevels[structure], mult[structure])))
                    {
                        barracksModels[buildingLevels[structure]].SetActive(true);
                        buildingLevels[structure] += 1;

                        GameManager.instance.attSpeedMultiplier = 1f / towerUpgrades[buildingLevels[structure]];
                        
                        for (int i = 0; i < barracksParticles.Length; i++)
                        {
                            barracksParticles[i].Play();
                        }

                        if (buildingLevels[structure] <= 4)
                            costs[structure].text = "" + CalculateCost(buildingLevels[structure], mult[structure]);
                        else
                        {
                            costs[structure].text = "MAX";
                            buildingMaxed[structure] = true;
                        }
                    }
                    else
                        Debug.Log("Insufficient _experience points to purchase Barracks Level " + buildingLevels[structure] + 1);
                }
                break;
        }
        
        ValueChanged();
        
        /*
        switch (structure)
        {
            
            case 0:     // WALL        WALL        WALL        WALL
                if (!buildingMaxed[0])
                {
                    if (RemoveExperience(CalculateCost(buildingLevels[0], mult[0])))
                    {
                        buildingLevels[0] += 1;

                        wallModels[buildingLevels[0]].SetActive(true);
                        GameManager.instance.healthMultiplier = wallUpgrades[buildingLevels[0]];

                        if (buildingLevels[0] <= 4)
                            costs[0].text = "" + CalculateCost(buildingLevels[0], mult[0]);
                        else
                        {
                            costs[0].text = "MAX";
                            buildingMaxed[0] = true;
                        }
                    }
                    else
                        Debug.Log("Insufficient _experience points to purchase Wall Level " + buildingLevels[0] + 1);
                }
                break;

            case 1:     // CHEST        CHEST       CHEST       CHEST
                if (!buildingMaxed[1])
                {
                    if (RemoveExperience(CalculateCost(buildingLevels[1], mult[1])))
                    {
                        buildingLevels[1] += 1;

                        chestModels[buildingLevels[1]].SetActive(true);
                        GameManager.instance.moneyMultiplier = chestUpgrades[buildingLevels[1]];
                    
                        if (buildingLevels[0] <= 4)
                            costs[1].text = "" + CalculateCost(buildingLevels[1], mult[1]);
                        else
                        {
                            costs[1].text = "MAX";
                            buildingMaxed[1] = true;
                        }
                    }
                    else
                        Debug.Log("Insufficient _experience points to purchase Wall Level " + buildingLevels[1] + 1);
                }

                break;

            case 2:     // TAVERN       TAVERN      TAVERN      TAVERN
                if (RemoveExperience(CalculateCost(buildingLevels[2], mult[2])))
                {
                    buildingLevels[2] += 1;

                    tavernModels[buildingLevels[2]].SetActive(true);
                    GameManager.instance.rangeMultiplier = rangeUpgrades[buildingLevels[2]];
                    
                    costs[2].text = "" + CalculateCost(buildingLevels[2], mult[2]);
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + buildingLevels[2] + 1);

                break;

            case 3:     // SMITH        SMITH       SMITH       SMITH
                if (RemoveExperience(CalculateCost(buildingLevels[3], mult[3])))
                {
                    buildingLevels[3] += 1;
                    
                    blacksmithModels[buildingLevels[3]].SetActive(true);
                    GameManager.instance.damageMultiplier = towerUpgrades[buildingLevels[3]];
                    
                    costs[3].text = "" + CalculateCost(buildingLevels[3], mult[3]);
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + buildingLevels[3] + 1);

                break;

            case 4:     // BARRACKS     BARRACKS        BARRACKS        BARRACKS
                if (RemoveExperience(CalculateCost(buildingLevels[4], mult[4])))
                {
                    buildingLevels[4] += 1;

                    barracksModels[buildingLevels[4]].SetActive(true);
                    GameManager.instance.attSpeedMultiplier = 1f / towerUpgrades[buildingLevels[4]];
                    
                    costs[4].text = "" + CalculateCost(buildingLevels[4], mult[4]);
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + buildingLevels[4] + 1);
                break;
        }
        */
        
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
        return buildingLevels[0];
    }

    public int GetChestLevel()
    {
        return buildingLevels[1];
    }

    public int GetTavernLevel()
    {
        return buildingLevels[2];
    }

    public int GetBlacksmithLevel()
    {
        return buildingLevels[3];
    }

    public int GetBarracksLevel()
    {
        return buildingLevels[4];
    }
}