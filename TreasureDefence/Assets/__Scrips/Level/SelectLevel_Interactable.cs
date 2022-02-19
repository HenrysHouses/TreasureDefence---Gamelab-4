using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel_Interactable : TD_Interactable
{
    public int levelNumber;

    [SerializeField] private string levelName;
    [TextArea(4,10)][SerializeField] private string levelInfo;
    [Range(1,6)][SerializeField] private int levelDifficulty = 1;

    override public void InteractTrigger(object target = null)
    {
        //PlayerInteraction player = target as PlayerInteraction;

        
        LevelSelector.instance.SetLevel(levelNumber, levelName, levelInfo, levelDifficulty);

    }

    override public void InteractionEndTrigger(object target = null)
    {
        
    }
}
