using UnityEngine;
using FMODUnity;

public class BuyTowerButton_Interactable : TD_Interactable
{
    [SerializeField] TowerInfo info;
    [SerializeField] Transform spawnTransform, displayPos;
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

            GameObject spawn = Instantiate(mesh, displayPos.position, Quaternion.identity);
            spawn.transform.SetParent(transform, true); 
            spawn.name = "Mesh_DisplayTower";
            Vector3 scaleOffset = new Vector3(0.02f, 0.02f, 0.02f);
            spawn.transform.localScale = spawn.transform.localScale - scaleOffset; 
            spawn.transform.Rotate(new Vector3(0, 180, 0), Space.World);
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

                Vector3 randomPos = spawnTransform.position;
                randomPos.x += Random.Range(0.5f, -0.5f);
                randomPos.z += Random.Range(0.07f, -0.07f);
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
        }
    }

    public void DeleteInfo()
    {
        if(DisplayInfo)
            Destroy(DisplayInfo);
    }
}
