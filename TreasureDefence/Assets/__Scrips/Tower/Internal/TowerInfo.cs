using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TreasureDefence/ShopItem")]
public class TowerInfo : ScriptableObject
{                  
	public int Type, cost;
	public GameObject item;
}
