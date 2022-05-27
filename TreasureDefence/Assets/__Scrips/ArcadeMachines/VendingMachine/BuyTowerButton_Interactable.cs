using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;

public class BuyTowerButton_Interactable : TD_Interactable
{
    [SerializeField] TowerInfo info;
    [SerializeField] Transform displayPos;
    [SerializeField] Transform[] spawnArea;
    GameObject DisplayInfo;
    [SerializeField] GameObject infoPrefab;
    [SerializeField] StudioEventEmitter _VendingMachineSFX;

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
            if(info.item)
            {
                for (int i = 0; i < info.item.transform.childCount; i++)
                {
                    if(info.item.transform.GetChild(i).name.Equals("Mesh"))
                        mesh = info.item.transform.GetChild(i).gameObject;
                }
            }

            if(mesh)
            {
                GameObject spawn = Instantiate(mesh, displayPos.position, Quaternion.identity);
                spawn.transform.SetParent(transform, true); 
                spawn.name = "Mesh_DisplayTower";
                Vector3 scaleOffset = new Vector3(0.02f, 0.02f, 0.02f);
                spawn.transform.localScale = spawn.transform.localScale - scaleOffset; 
                spawn.transform.Rotate(new Vector3(0, 180, 0), Space.World);
            }
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
        if(info)
        {
            if(CurrencyManager.instance.SubtractMoney(info.cost))
            {   //Valid Sound
                if (!FmodExtensions.IsPlaying(_VendingMachineSFX.EventInstance))
                {
                    _VendingMachineSFX.Play();
                    _VendingMachineSFX.SetParameter("Valid_Invalid", 0);
                }

                float randomX = Random.Range(0,1000);
                randomX = ExtensionMethods.Remap(randomX, 0, 1000, 0, 1);
                float randomY = Random.Range(0,1000);
                randomY = ExtensionMethods.Remap(randomY, 0, 1000, 0, 1);


                Vector3 x = Vector3.Lerp(spawnArea[0].position, spawnArea[1].position, randomX);
                Vector3 y = Vector3.Lerp(spawnArea[1].position, spawnArea[2].position, randomY);                
                Vector3 randomPos = spawnArea[0].position - (x-y);

                if(info.item)
                    GameManager.instance.SpawnTower(info.item, randomPos);
            }
            else
            {
                //Invalid sound
                _VendingMachineSFX.Play();
                _VendingMachineSFX.SetParameter("Valid_Invalid", 1);
            }
        }
    }

    public void SpawnInfo()
    {
        if(info)
        {
            DisplayInfo = Instantiate(infoPrefab, displayPos.position, Quaternion.identity);
            DisplayInfo.GetComponent<DisplayInfo>().setText(info);
            DisplayInfo.transform.rotation = transform.rotation;
        }
    }

    public void DeleteInfo()
    {
        if(DisplayInfo)
            Destroy(DisplayInfo);
    }

    /// <summary> Smoothly lerps from the Start position to the End position </summary>
    /// <param name="Start">Position of the lerp when interpolation is 0</param>
    /// <param name="End">Position of the lerp when interpolation is 1</param>
    /// <param name="TimeStarted">Static variable Time.time when the lerp began</param>
    /// <param name="Interval">Time between the interpolation goes 0 to 1.</param>
    /// <returns>Vector3 Position between Start and End</returns>
    private Vector3 LerpHelper(Vector3 Start, Vector3 End, float TimeStarted, float Interval = 1)
    {
        //calculates a new lerp location from 0-1 based on how much time has passed since the lerp started
        float TimePassed = Time.time - TimeStarted;
        float lerpLocation = TimePassed / Interval;
    
        //returns new lerped position
        return Vector3.Lerp(Start, End, lerpLocation);
    }
}
