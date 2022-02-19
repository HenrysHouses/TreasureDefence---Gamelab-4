using UnityEngine;

public class VRControllerInputData
{
    public bool triggerValue;
    public bool gripValue;
    public bool primaryButtonValue;

    public bool isFist => triggerValue && gripValue && primaryButtonValue;
    public bool isNoInputs()
    {
        if(triggerValue)
            return false;
        if(gripValue)
            return false;
        if(primaryButtonValue)
            return false;
        return true;
    }

    public void print()
    {
        string msg = "";
        msg += "triggerValue: " + triggerValue; 
        msg += "\ngripValue: " + gripValue; 
        msg += "\nprimaryButtonValue: " + primaryButtonValue; 
        Debug.Log(msg);
    }
}
