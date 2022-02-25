using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class VR_GestureHandler : MonoBehaviour
{
    InputDevice targetDevice;
    [SerializeField] InputDeviceCharacteristics _ControllerCharacteristic;
    // Interaction stuff 
    [SerializeField] VRControllerState _controllerState;
    [SerializeField] VRControllerInputData _inputs;
    [SerializeField] GameObject targetRay, targetController;
    bool isBusy;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
		InputDevices.GetDevicesWithCharacteristics(_ControllerCharacteristic, devices);

		foreach(var item in devices)
		{
			Debug.Log(item.name + item.characteristics);
		}

        if(devices.Count > 0)
            targetDevice = devices[0];
        Debug.Log(targetDevice.name);
    }

    // Update is called once per frame
    void Update()
    {
        _inputs = handleControllerInputs();

        _controllerState = checkControllerState(_inputs);

        // point to use the ray
        if(_controllerState == VRControllerState.Point && targetRay.activeSelf)
        {
            targetRay.SetActive(true);
        }
        else if(_controllerState != VRControllerState.Point && !targetRay.activeSelf)
        {
            targetRay.SetActive(false);
        }
    }

    VRControllerState checkControllerState(VRControllerInputData inputs)
    {
        if(inputs.triggerValue && inputs.gripValue && inputs.primaryButtonValue)
            return VRControllerState.Fist;
        if(inputs.gripValue && inputs.primaryButtonValue)
            return VRControllerState.Point;
        if(inputs.gripValue)
            return VRControllerState.FingerGun;
        return VRControllerState.Grip;
    }

    VRControllerInputData handleControllerInputs()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue);
        targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue);
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);

        VRControllerInputData data = new VRControllerInputData();
        data.gripValue = gripValue;
        data.triggerValue = triggerValue;
        data.primaryButtonValue = primaryButtonValue;

        return data;
    }

}

public enum VRControllerState
{
	Grip,
	FingerGun,
	Point,
	ThumbsUp,
	Fist,
    Any
}