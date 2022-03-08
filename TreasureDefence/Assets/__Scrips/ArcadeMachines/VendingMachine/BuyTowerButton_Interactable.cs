using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTowerButton_Interactable : TD_Interactable
{
    [SerializeField] TowerInfo info;
    [SerializeField] Transform spawnTransform, displayPos;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    new void Start()
    {
        base.Start();
        if(info)
        {
            GameObject mesh = null;
            for (int i = 0; i < info.item.transform.childCount; i++)
            {
                if(info.item.transform.GetChild(i).name.Equals("Mesh"))
                    mesh = info.item.transform.GetChild(i).gameObject;
            }

            GameObject spawn = Instantiate(mesh, displayPos.position, Quaternion.identity);
            spawn.transform.SetParent(transform, true); 
            spawn.name = "Mesh_DisplayTower";
        }
    }

    public override void InteractionStartTrigger(object target = null)
    {
        buyTower();
    }

    public override void VRInteractionStartTrigger()
    {
        buyTower();
    }

    private void buyTower()
    {
        if(info.item)
        {
            if(CurrencyManager.instance.SubtractMoney(info.cost))
            {
                Vector3 randomPos = spawnTransform.position;
                randomPos.x += Random.Range(0.5f, -0.5f);
                randomPos.z += Random.Range(0.07f, -0.07f);
                Instantiate(info.item, randomPos, Quaternion.identity);
            }
        }
    }
}
