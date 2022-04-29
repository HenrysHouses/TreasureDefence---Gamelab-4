using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCoveController : MonoBehaviour
{
    public static PirateCoveController instance;
    
    private int _experience;

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
    [SerializeField] private GameObject[] guardTowerModels;

    private float[] wallUpgrades = { 1f, 1.25f, 1.5f,  1.9f, 2.4f, 3f };

    private float[] towerUpgrades = { 1f, 1.1f, 1.23f,  1.44f, 1.67f, 2f };
    
    private float[] rangeUpgrades = { 1f, 1.05f, 1.12f,  1.2f, 1.3f, 1.45f };

    private float[] chestUpgrades = { 1f, 1.1f, 1.23f,  1.44f, 1.67f, 2f };


    // Upgrades
    private int _wallLevel;
    private int _chestLevel;
    private int _tavernLevel;
    private int _blacksmithLevel;
    private int _barracksLevel;
    private int _guardTowerLevel;

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
        
        for (int i = 0; i < 10; i++)
        {
            int temp = i + 1;

            Debug.Log("Level " + temp + " : " + CalculateCost(i) + " xp.");
        }
    }

    private void Update()
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
        
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Upgrade(5);
        }
    }

    public int CalculateCost(int structureLevel)
    {
        int val = (structureLevel + 1) * 5;

        return Mathf.FloorToInt(Mathf.Pow(val, 2.22f)) + 1;
    }

    public void Upgrade(int structure)
    {
        switch (structure)
        {
            case 0:
                if (RemoveExperience(CalculateCost(_wallLevel)))
                {
                    //wallModels[_wallLevel].SetActive(false);
                    
                    _wallLevel += 1;

                    wallModels[_wallLevel].SetActive(true);
                    GameManager.instance.healthMultiplier = wallUpgrades[_wallLevel];
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + _wallLevel + 1);

                break;

            case 1:
                if (RemoveExperience(CalculateCost(_chestLevel)))
                {
                    //chestModels[_chestLevel].SetActive(false);
                    
                    _chestLevel += 1;

                    chestModels[_chestLevel].SetActive(true);
                    GameManager.instance.moneyMultiplier = chestUpgrades[_chestLevel];
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + _chestLevel + 1);

                break;

            case 2:
                if (RemoveExperience(CalculateCost(_tavernLevel)))
                {
                    //tavernModels[_tavernLevel].SetActive(false);
                    
                    _tavernLevel += 1;

                    tavernModels[_tavernLevel].SetActive(true);
                    GameManager.instance.rangeMultiplier = rangeUpgrades[_tavernLevel];
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + _tavernLevel + 1);

                break;

            case 3:
                if (RemoveExperience(CalculateCost(_blacksmithLevel)))
                {
                    //blacksmithModels[_blacksmithLevel].SetActive(false);
                    
                    _blacksmithLevel += 1;
                    
                    blacksmithModels[_blacksmithLevel].SetActive(true);
                    GameManager.instance.damageMultiplier = towerUpgrades[_blacksmithLevel];
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + _blacksmithLevel + 1);

                break;

            case 4:
                if (RemoveExperience(CalculateCost(_barracksLevel)))
                {
                    //barracksModels[_barracksLevel].SetActive(false);
                    
                    _barracksLevel += 1;

                    barracksModels[_barracksLevel].SetActive(true);
                    GameManager.instance.attSpeedMultiplier = 1f / towerUpgrades[_barracksLevel];
                }
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + _barracksLevel + 1);
                break;

            case 5:
                if (RemoveExperience(CalculateCost(_guardTowerLevel)))
                {
                    //guardTowerModels[_guardTowerLevel].SetActive(false);
                    
                    _guardTowerLevel += 1;
                    guardTowerModels[_guardTowerLevel].SetActive(true);
                    //Turret.instance.SetLevel(_guardTowerLevel);
                }
                    
                else
                    Debug.Log("Insufficient _experience points to purchase Wall Level " + _guardTowerLevel + 1);
                break;
        }
    }


    public void AddExperience(int num = 1)
    {
        _experience += num;
    }

    public bool RemoveExperience(int num)
    {
        if (_experience - num < 0) return false;

        _experience -= num;
        return true;
    }

    public int GetWallLevel()
    {
        return _wallLevel;
    }

    public int GetChestLevel()
    {
        return _chestLevel;
    }

    public int GetTavernLevel()
    {
        return _tavernLevel;
    }

    public int GetBlacksmithLevel()
    {
        return _blacksmithLevel;
    }

    public int GetBarracksLevel()
    {
        return _barracksLevel;
    }

    public int GetGuardTowerLevel()
    {
        return _guardTowerLevel;
    }
}