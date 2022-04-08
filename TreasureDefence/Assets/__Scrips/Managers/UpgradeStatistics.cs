using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TreasureDefence/Upgrade Statistics")]
public class UpgradeStatistics : ScriptableObject
{
    public float attackDamageMultiplier;
    public float attackSpeedMultiplier;
    public float attackRangeMultiplier;
    public float goldGainedMultiplier;

    public int pirateCoveHealth;
    
    // I don't think we need guard tower here?

}
