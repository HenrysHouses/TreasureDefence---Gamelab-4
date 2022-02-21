using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class TD_VR_Interaction : XRBaseControllerInteractor
{




    // public InputDevice targetDevice;
    // [SerializeField] InputDeviceCharacteristics controllerCharacteristic;
    // GameObject spawnedController;
    // [SerializeField] Collider gripTrigger;
    // [SerializeField] Collider pointTrigger;
    // [SerializeField] Collider fingerGunTrigger;
    // [SerializeField] Collider fistTrigger;

    // // Interaction stuff 
    // [SerializeField] TD_Interactable foundInteraction;
    // [SerializeField] VRInteractionMethod interactionType;
    // bool isBusy;

    // Start is called before the first frame update
    // void Start()
    // {
    //     List<InputDevice> devices = new List<InputDevice>();
	// 	InputDeviceCharacteristics rightControllerCharacteristics = controllerCharacteristic;
	// 	InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

	// 	foreach(var item in devices)
	// 	{
	// 		Debug.Log(item.name + item.characteristics);
	// 	}

    //     if(devices.Count > 0)
    //         targetDevice = devices[0];
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     VRControllerInputData inputs = handleControllerInputs();

    //     if(isBusy)
    //     {
    //         Debug.Log(inputs.isNoInputs());
    //         // inputs.print();
    //     }

    //     if(!isBusy)
    //     {
    //         interactionType = checkControllerState(inputs);

    //         switch(interactionType) // we can have different triggers enabled for the different types so that they have different trigger positions
    //         {
    //             case VRInteractionMethod.Grip:
    //                 if(inputs.triggerValue && inputs.gripValue && !inputs.primaryButtonValue)
    //                     startInteraction(VRInteractionMethod.Grip);
    //                 break;

    //             case VRInteractionMethod.Point:
    //                 if(inputs.triggerValue)
    //                     startInteraction(VRInteractionMethod.Point);
    //                 break;

    //             case VRInteractionMethod.FingerGun:
    //                 if(!inputs.triggerValue && inputs.primaryButtonValue && inputs.gripValue)
    //                     startInteraction(VRInteractionMethod.FingerGun);
    //                 break;

    //             case VRInteractionMethod.ThumbsUp:
    //                 if(inputs.primaryButtonValue && inputs.triggerValue && inputs.gripValue)
    //                     startInteraction(VRInteractionMethod.ThumbsUp);
    //                 break;

    //             case VRInteractionMethod.Fist:
    //                 Debug.Log("this method is not implemented");
    //                 // if(inputs.isFist)
    //                 //     startInteraction(VRInteractionMethod.Fist);
    //                 break;
    //         }
    //     }
    //     else if(inputs.isNoInputs())
    //     {
    //         endInteraction();
    //     }
    // }

    // void OnTriggerEnter(Collider other)
    // {
    //     foundInteraction = other.GetComponent<TD_Interactable>();
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if(foundInteraction)
    //         if(other == foundInteraction.getCollider() && !isBusy)
    //             foundInteraction = null;
    // }

    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("collision");
    // }

    // void startInteraction(VRInteractionMethod method)
    // {
    //     if(foundInteraction)
    //     {
    //         Debug.Log(method.ToString());
    //         if(foundInteraction.interactionMethod == method)
    //         {
    //             foundInteraction.InteractTrigger(method.ToString());
    //             isBusy = true;

    //             // if(method == VRInteractionMethod.Fist)
    //             //     endInteraction();
    //         }
    //     }
    // }

    // void endInteraction()
    // {
    //     foundInteraction.InteractionEndTrigger();
    //     isBusy = false;
    // }

    // VRInteractionMethod checkControllerState(VRControllerInputData inputs)
    // {
    //     if(inputs.triggerValue && inputs.gripValue && inputs.primaryButtonValue)
    //         return VRInteractionMethod.Fist;
    //     if(inputs.gripValue && inputs.primaryButtonValue)
    //         return VRInteractionMethod.Point;
    //     if(inputs.gripValue)
    //         return VRInteractionMethod.FingerGun;
    //     return VRInteractionMethod.Grip;
    // }

    // VRControllerInputData handleControllerInputs()
    // {
    //     targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue);
    //     targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue);
    //     targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);

    //     VRControllerInputData data = new VRControllerInputData();
    //     data.gripValue = gripValue;
    //     data.triggerValue = triggerValue;
    //     data.primaryButtonValue = primaryButtonValue;

    //     return data;
    // }

    // void transitionToControllerState(VRInteractionMethod transitionFrom, VRInteractionMethod transitionTo)
    // {
    //     // return null;
    // }
}

// public enum VRInteractionMethod
// {
// 	Grip,
// 	FingerGun,
// 	Point,
// 	ThumbsUp,
// 	Fist,
//     Any
// }