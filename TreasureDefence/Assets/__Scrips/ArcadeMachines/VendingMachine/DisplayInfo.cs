using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInfo : MonoBehaviour
{
    [SerializeField] TextMeshPro TowerNameTXT;
    [SerializeField] TextMeshPro DamageTXT;
    [SerializeField] TextMeshPro TargetingTXT;
    [SerializeField] TextMeshPro ProjectileTXT;
    [SerializeField] TextMeshPro InfoTXT;
    [SerializeField] TextMeshPro CostTXT;

    public void setText(TowerInfo info)
    {
        TowerNameTXT.text += info.TowerName;
        DamageTXT.text += info.Damage;
        TargetingTXT.text += info.TargetingType.ToString();
        ProjectileTXT.text += info.Projectile;
        InfoTXT.text += info.ExtraInfo;
        CostTXT.text += info.cost.ToString();
    }
}
