using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCoveController : MonoBehaviour
{
    private int experience;

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

    private float[] chestUpgrades = { 1f, 1.1f, 1.23f,  1.44f, 1.67f, 2f };


    // Upgrades
    private int wallLevel;
    private int chestLevel;
    private int tavernLevel;
    private int blacksmithLevel;
    private int barracksLevel;
    private int guardTowerLevel;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            int temp = i + 1;

            Debug.Log("Level " + temp + " : " + CalculateCost(i) + " xp.");
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
                if (RemoveExperience(CalculateCost(wallLevel)))
                {
                    wallModels[wallLevel].SetActive(false);
                    
                    wallLevel += 1;

                    wallModels[wallLevel].SetActive(true);
                    GameManager.instance.healthMultiplier = wallUpgrades[wallLevel];
                }
                else
                    Debug.Log("Insufficient experience points to purchase Wall Level " + wallLevel + 1);

                break;

            case 1:
                if (RemoveExperience(CalculateCost(chestLevel)))
                {
                    chestModels[chestLevel].SetActive(false);
                    
                    chestLevel += 1;

                    chestModels[chestLevel].SetActive(true);
                    GameManager.instance.moneyMultiplier = chestUpgrades[chestLevel];
                }
                else
                    Debug.Log("Insufficient experience points to purchase Wall Level " + chestLevel + 1);

                break;

            case 2:
                if (RemoveExperience(CalculateCost(tavernLevel)))
                {
                    tavernModels[tavernLevel].SetActive(false);
                    
                    tavernLevel += 1;

                    tavernModels[tavernLevel].SetActive(true);
                    GameManager.instance.rangeMultiplier = towerUpgrades[tavernLevel];
                }
                else
                    Debug.Log("Insufficient experience points to purchase Wall Level " + tavernLevel + 1);

                break;

            case 3:
                if (RemoveExperience(CalculateCost(blacksmithLevel)))
                {
                    blacksmithModels[blacksmithLevel].SetActive(false);
                    
                    blacksmithLevel += 1;
                    
                    blacksmithModels[blacksmithLevel].SetActive(true);
                    GameManager.instance.damageMultiplier = towerUpgrades[blacksmithLevel];
                }
                else
                    Debug.Log("Insufficient experience points to purchase Wall Level " + blacksmithLevel + 1);

                break;

            case 4:
                if (RemoveExperience(CalculateCost(barracksLevel)))
                {
                    barracksModels[barracksLevel].SetActive(false);
                    
                    barracksLevel += 1;

                    barracksModels[barracksLevel].SetActive(true);
                    GameManager.instance.attSpeedMultiplier = towerUpgrades[barracksLevel];
                }
                else
                Debug.Log("Insufficient experience points to purchase Wall Level " + barracksLevel + 1);
                break;

            case 5:
                if (RemoveExperience(CalculateCost(guardTowerLevel)))
                    guardTowerLevel += 1;
                else
                    Debug.Log("Insufficient experience points to purchase Wall Level " + guardTowerLevel + 1);
                break;
        }
    }


    public void AddExperience(int num = 1)
    {
        experience += num;
    }

    public bool RemoveExperience(int num)
    {
        if (experience - num < 0) return false;

        experience -= num;
        return true;
    }

    public int GetWallLevel()
    {
        return wallLevel;
    }

    public int GetChestLevel()
    {
        return chestLevel;
    }

    public int GetTavernLevel()
    {
        return tavernLevel;
    }

    public int GetBlacksmithLevel()
    {
        return blacksmithLevel;
    }

    public int GetBarracksLevel()
    {
        return barracksLevel;
    }

    public int GetGuardTowerLevel()
    {
        return guardTowerLevel;
    }
}