using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeValues : MonoBehaviour
{
    public static UpgradeValues instance;

    public float healthMultiplier;
    public float moneyMultiplier;
    public float rangeMultiplier;
    public float damageMultiplier;
    public float attSpeedMultiplier;

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

    public float GetHealthMultiplier()
    {
        return healthMultiplier;
    }

    public float GetMoneyMultiplier()
    {
        return moneyMultiplier;
    }

    public float GetRangeMultiplier()
    {
        return rangeMultiplier;
    }

    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }
}