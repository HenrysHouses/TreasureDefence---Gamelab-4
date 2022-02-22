using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class TD_VR_Interaction : MonoBehaviour
{
    [SerializeField]
    public InputDevice targetDevice;
    [SerializeField] InputDeviceCharacteristics controllerCharacteristic;
    GameObject spawnedController;
    [SerializeField] Collider gripTrigger;
    [SerializeField] Collider pointTrigger;
    [SerializeField] Collider fingerGunTrigger;
    [SerializeField] Collider fistTrigger;

    // Interaction stuff 
    [SerializeField] TD_Interactable foundInteraction;
    [SerializeField] VRControllerState _controllerState;
    [SerializeField] VRInteractionMethod interactionType = VRInteractionMethod.None;
    [SerializeField] VRControllerInputData inputs;
    bool isBusy, triggerInteraction;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
		InputDeviceCharacteristics rightControllerCharacteristics = controllerCharacteristic;
		InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

		foreach(var item in devices)
		{
			Debug.Log(item.name + item.characteristics);
		}

        if(devices.Count > 0)
            targetDevice = devices[0];
    }

    // Update is called once per frame
    void Update()
    {
        inputs = handleControllerInputs();

        // inputs.print();

        if(isBusy)
        {
            Debug.Log(inputs.isNoInputs());
            // inputs.print();
        }

        if(!isBusy)
        {
            VRControllerState previousState = _controllerState;
            VRControllerState transitionToState = checkControllerState(inputs);
            _controllerState = transitionToControllerState(previousState, transitionToState, out interactionType);
            Debug.Log(interactionType);

            startInteraction(interactionType);
        }
        else if(inputs.isNoInputs())
        {
            endInteraction();
        }
        triggerInteraction = false;
    }

    void OnTriggerEnter(Collider other)
    {
        foundInteraction = other.GetComponent<TD_Interactable>();
    }

    void OnTriggerExit(Collider other)
    {
        if(foundInteraction)
            if(other == foundInteraction.getCollider() && !isBusy)
                foundInteraction = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }

    void startInteraction(VRInteractionMethod method)
    {
        if(foundInteraction)
        {
        Debug.Log("starting");
            Debug.Log(method.ToString());
            // if(foundInteraction.interactionMethod == method)
            // {
            //     foundInteraction.InteractTrigger(method.ToString());
            //     isBusy = true;

            //     // if(method == VRInteractionMethod.Fist)
            //     //     endInteraction();
            // }
        }
    }

    void endInteraction()
    {
        foundInteraction.InteractionEndTrigger();
        isBusy = false;
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

    VRControllerState transitionToControllerState(VRControllerState transitionFrom, VRControllerState transitionTo, out VRInteractionMethod interactionType)
    {
        if(transitionFrom == VRControllerState.Grip && transitionTo == VRControllerState.ThumbsUp)
        {
            interactionType = VRInteractionMethod.Grab;
            return VRControllerState.ThumbsUp;
        }
        if (transitionFrom == VRControllerState.ThumbsUp && transitionTo == VRControllerState.Fist)
        {
            interactionType = VRInteractionMethod.ThumbTrigger;
            return VRControllerState.Fist;
        }
        if (transitionFrom == VRControllerState.FingerGun && transitionTo == VRControllerState.Point)
        {
            interactionType = VRInteractionMethod.FingerShoot;
            return VRControllerState.Point;
        }
        if (transitionFrom == VRControllerState.Point && transitionTo == VRControllerState.Fist)
        {
            interactionType = VRInteractionMethod.PointPush;
            return VRControllerState.Fist;
        }
        if (transitionFrom == VRControllerState.Grip && transitionTo == VRControllerState.Fist)
        {
            interactionType = VRInteractionMethod.ClenchFist;
            return VRControllerState.Fist;
        }
        interactionType = VRInteractionMethod.None;
        return transitionTo;
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

public enum VRInteractionMethod
{
    Grab,
    FingerShoot,
    ThumbTrigger,
    ClenchFist,
    PointPush,
    None
}