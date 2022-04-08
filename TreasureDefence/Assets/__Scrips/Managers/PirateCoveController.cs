using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCoveController : MonoBehaviour
{

    private int experience;
    
    /*
     ____________________________________________________________________
    |                                                                   |
    |    Wall            Decides pirate cove HP                         |
    |    Chest           Increase money gained from enemies             |
    |    Tavern          Increase tower damage                          |
    |    Shooting Range  Increase tower range                           |
    |    Barracks        Increase tower attack speed                    |
    |    Guard Tower     Spot for permanent tower that can be upgraded  |  
    |___________________________________________________________________| 

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
    [SerializeField] private GameObject shootingRange;
    [SerializeField] private GameObject barracks;
    [SerializeField] private GameObject guardTower;
    */

    [SerializeField] private GameObject[] wallModels;
    [SerializeField] private GameObject[] chestModels;
    [SerializeField] private GameObject[] tavernModels;
    [SerializeField] private GameObject[] shootingRangeModels;
    [SerializeField] private GameObject[] barracksModels;
    [SerializeField] private GameObject[] guardTowerModels;
    
    // Upgrades
    private int wallLevel;
    private int chestLevel;
    private int tavernLevel;
    private int shootingRangeLevel;
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
        int val = (structureLevel+1) * 5;
        
        return Mathf.FloorToInt(Mathf.Pow(val, 2.22f)) + 1;
    }

    public void Upgrade(int structure)
    {
        switch (structure)
        {
            case 0:
                if (RemoveExperience(CalculateCost(wallLevel)))
                    wallLevel += 1;
                else
                    Debug.Log("Insufficient experience points to purchase Wall Level " + wallLevel + 1);
                break;
            
            case 1:
                if (RemoveExperience(CalculateCost(chestLevel)))
                    chestLevel += 1;
                else
                    Debug.Log("Insufficient experience points to purchase Wall Level " + chestLevel + 1);
                break;
            
            case 2:
                if (RemoveExperience(CalculateCost(tavernLevel)))
                    tavernLevel += 1;
                else
                    Debug.Log("Insufficient experience points to purchase Wall Level " + tavernLevel + 1);
                break;
            
            case 3:
                if (RemoveExperience(CalculateCost(shootingRangeLevel)))
                    shootingRangeLevel += 1;
                else
                    Debug.Log("Insufficient experience points to purchase Wall Level " + shootingRangeLevel + 1);
                break;
            
            case 4:
                if (RemoveExperience(CalculateCost(barracksLevel)))
                    barracksLevel += 1;
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
    
    public int GetShootingRangeLevel()
    {
        return shootingRangeLevel;
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
