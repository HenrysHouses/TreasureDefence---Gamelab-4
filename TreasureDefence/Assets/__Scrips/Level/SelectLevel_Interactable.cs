using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel_Interactable : TD_Interactable
{
    public int levelNumber;
    Animator animator;
    [SerializeField] private string levelName;
    [TextArea(4,10)][SerializeField] private string levelInfo;
    [Range(1,6)][SerializeField] private int levelDifficulty = 1;

    [SerializeField] StudioEventEmitter mapdropSFX;

    new void Start()
    {
       base.Start();
       animator = GetComponent<Animator>();
    }

    override public void InteractionStartTrigger(object target = null)
    {
        //PlayerInteraction player = target as PlayerInteraction;
        
        LevelSelector.instance.SetLevel(levelNumber, levelName, levelInfo, levelDifficulty);

        if(GameManager.instance)
            GameManager.instance.LevelPickupHighlight.enabled = true;

        mapdropSFX.Play();
    }

    override public void LookInteraction()
    {
        // if(lookIsActive && !animator.GetBool("IsHovering"))
        // {
        //     // animator.SetBool("IsHovering", true);
        // }
        // if(!lookIsActive && animator.GetBool("IsHovering"))
        //     // animator.SetBool("IsHovering", false);
    }
    

	override public void VRInteractionStartTrigger()
    {
        LevelSelector.instance.SetLevel(levelNumber, levelName, levelInfo, levelDifficulty);
        Debug.Log("PlayingSFX?");
        mapdropSFX.Play();

    }

}
