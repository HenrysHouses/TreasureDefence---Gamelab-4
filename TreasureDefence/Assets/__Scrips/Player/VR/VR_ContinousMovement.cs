using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
public class VR_ContinousMovement : MonoBehaviour
{
    public GameObject movementNodeHighlight, rotationNodeHighlight;

    public float speed, turnSpeed, snapRotationAmount = 45, snapRotationCooldown = 0.3f, heightChangeAmount = 0.3f, heightCooldown = 0.3f;
    public XRNode movementNode, rotationNode;
    Vector2 moveInput, rotateInput;
    bool rotateOverride, canRotate = true;
    bool secondaryButton_RightHand, primaryButton_RightHand, isRotating;
    bool ChangeHeight, heightIsChanging;
    bool movementTracker, rotationTracker;
    CharacterController character;
    private XROrigin rig;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        // All movement inputs
        InputDevice device = InputDevices.GetDeviceAtXRNode(movementNode);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out moveInput);
        device.TryGetFeatureValue(CommonUsages.primaryButton, out rotateOverride);
        
        // other inputs
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out ChangeHeight);
        device.TryGetFeatureValue(CommonUsages.userPresence, out movementTracker);

        device = InputDevices.GetDeviceAtXRNode(rotationNode);
        device.TryGetFeatureValue(CommonUsages.userPresence, out rotationTracker);

        // All rotation inputs
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out rotateInput);    
        device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButton_RightHand);
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButton_RightHand);

        // Highlight controllers if they are not tracked // ! Not working yet, do this later if feel like it
        // if(!rotationTracker && !rotationNodeHighlight.activeSelf)
        // {
        //     rotationNodeHighlight.SetActive(true);
        // }
        // else if(rotationTracker && rotationNodeHighlight.activeSelf)
        // {
        //     rotationNodeHighlight.SetActive(false);
        // }
        
        // if(!movementTracker && !movementNodeHighlight.activeSelf)
        // {
        //     movementNodeHighlight.SetActive(true);

        // }
        // else if(movementTracker && movementNodeHighlight.activeSelf)
        // {
        //     movementNodeHighlight.SetActive(false);

        // }

        if(!isRotating && !ChangeHeight) // snap rotation
        {
            if(secondaryButton_RightHand)
            {
                Vector3 rotation = new Vector3(0, -snapRotationAmount, 0);
                StartCoroutine(snapRotate(rotation));
            }

            if(primaryButton_RightHand)
            {
                Vector3 rotation = new Vector3(0, snapRotationAmount, 0);
                StartCoroutine(snapRotate(rotation));
            }
        }

        if(ChangeHeight)
        {
            if(secondaryButton_RightHand && !heightIsChanging)
            {
                StartCoroutine(updateHeight(heightChangeAmount));
            }

            if(primaryButton_RightHand && !heightIsChanging)
            {
                float direction = heightChangeAmount *-1;
                StartCoroutine(updateHeight(direction));
            }
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(moveInput.x, 0, moveInput.y);

        character.Move(direction * Time.fixedDeltaTime * speed);

        if(canRotate || rotateOverride) // continuos rotation
        {
            Vector3 rotation = new Vector3(0, rotateInput.x, 0);
            transform.Rotate(rotation, Space.World);
        }
    }

    IEnumerator snapRotate(Vector3 rotation)
    {
        isRotating = true;
        transform.Rotate(rotation, Space.World);
        yield return new WaitForSeconds(snapRotationCooldown);
        isRotating= false;
    }

    IEnumerator updateHeight(float amount)
    {
        heightIsChanging = true;
        rig.CameraYOffset += amount;
        yield return new WaitForSeconds(heightCooldown);
        heightIsChanging= false;
    }

    public void setCanRotate(bool state)
    {
        canRotate = state;
    }
}
