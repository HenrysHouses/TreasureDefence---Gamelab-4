using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TreasureDefence/ShopItem")]
public class TowerInfo : ScriptableObject
{                  
	public int Type, cost;
	public GameObject item;
	public string TowerName, Damage, Projectile, ExtraInfo;
	public TowerBehaviour.TargetType TargetingType;

	void OnValidate()
	{
		if(this.item)
		{
			this.TargetingType = this.item.GetComponent<TowerBehaviour>().targetType;
			this.Damage = this.item.GetComponent<TowerBehaviour>().attackDamage.ToString();
		}
	}

}
