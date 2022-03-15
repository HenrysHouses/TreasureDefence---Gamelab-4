using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
public class VR_PlayerController : MonoBehaviour
{
    [SerializeField] GameObject teleportRay, pickupRayLeft, pickupRayRight, handLeft, handRight;
    [SerializeField] GameObject TeleportInteractor_Highlight;
    Renderer teleportHighlight;
    CharacterController character;
    public float speed;
    bool hasTeleported, shouldTeleport;
    Vector3 teleportPos;
    public float snapRotationCooldown = 0.3f, heightChangeAmount = 0.3f, heightCooldown = 0.3f;
    public XRNode leftNode, rightNode;
    Vector2 rightAxis, leftAxis;
    bool leftAxisClick;
    bool secondaryButton_RightHand, primaryButton_RightHand;
    bool isRotating;
    bool secondaryButton_LeftHand, primaryButton_LeftHand;
    bool heightIsChanging;
    private XROrigin rig;

    public bool leftRayIsActive, rightRayIsActive, leftHandIsActive, rightHandIsActive;
    public bool canMove = true, canTeleport = true, canRotate = true;

    XRController leftRayController, rightRayController, leftHandController, rightHandController;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
        teleportHighlight = TeleportInteractor_Highlight.GetComponentInChildren<Renderer>();

        leftRayController = pickupRayLeft.GetComponent<XRController>();
        rightRayController = pickupRayRight.GetComponent<XRController>();
        leftHandController = handLeft.GetComponent<XRController>();
        rightHandController = handRight.GetComponent<XRController>();
    }

    // Update is called once per frame
    void Update()
    {
        // get controller inputs
        InputDevice LeftDevice = InputDevices.GetDeviceAtXRNode(leftNode);
        LeftDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButton_LeftHand);
        LeftDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButton_LeftHand);
        LeftDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftAxis);
        LeftDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out leftAxisClick);
        InputDevice RightDevice = InputDevices.GetDeviceAtXRNode(rightNode);
        RightDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightAxis);
        RightDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButton_RightHand);
        RightDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButton_RightHand);

        // activate move held item in right hand
        if(secondaryButton_LeftHand && rightHandController.moveObjectOut != InputHelpers.Button.PrimaryAxis2DUp)
        {
            XRController[] controllers = new XRController[]{rightRayController, rightHandController};
            enableJoystickMove(controllers, true);
            canRotate = false;
        }
        else if (!secondaryButton_LeftHand && rightHandController.moveObjectOut != InputHelpers.Button.None)
        {
            XRController[] controllers = new XRController[]{rightRayController, rightHandController};
            enableJoystickMove(controllers, false);
            canRotate = true;
        }
        // activate move held item in left hand
        if(secondaryButton_RightHand && leftHandController.moveObjectOut != InputHelpers.Button.PrimaryAxis2DUp)
        {
            XRController[] controllers = new XRController[]{leftRayController, leftHandController};
            enableJoystickMove(controllers, true);
            canMove = false;
        }
        else if (!secondaryButton_RightHand && leftHandController.moveObjectOut != InputHelpers.Button.None)
        {
            XRController[] controllers = new XRController[]{leftRayController, leftHandController};
            enableJoystickMove(controllers, false);
            canMove = true;
        }

        if(leftAxisClick)
        {
            resetPosition();
        }

        // teleporting
        if(primaryButton_RightHand)
        {
            if(!teleportRay.activeSelf)
            {
                // show teleport interactor
                teleportRay.SetActive(true);
                // hide hands
                if(!leftHandIsActive)
                    handLeft.SetActive(false);
                if(!rightHandIsActive)
                    handRight.SetActive(false);
            }
            else if(primaryButton_LeftHand && !hasTeleported && canTeleport)
            {
                if(teleportHighlight.material.color != Color.white)
                {
                    teleportPos = TeleportInteractor_Highlight.transform.position;
                    shouldTeleport  = true;
                }
            }
        }
        else // ray pickup
        {
            if(primaryButton_LeftHand)
            {
                if(!pickupRayLeft.activeSelf)
                {
                    // show ray interactors
                        pickupRayLeft.SetActive(true);
                        handLeft.SetActive(false);
                        pickupRayRight.SetActive(true);
                        handRight.SetActive(false);
                }
            }   
        }
        
        if(!primaryButton_LeftHand && !primaryButton_RightHand)
        {
            // show hands
            if(!handLeft.activeSelf)
            {
                handLeft.SetActive(true);
                handRight.SetActive(true);
                // hide ray interactor
                pickupRayLeft.SetActive(false);
                pickupRayRight.SetActive(false);
                // hide teleport interactor
                teleportRay.SetActive(false);
            }
        }
        if(!primaryButton_LeftHand)
        {
            if(hasTeleported)
                hasTeleported = false;
        }
       
        // rotate
        if(canRotate)
        {
            if(rightAxis.x > 0.1f && !isRotating)
            {
                Vector3 rotation = new Vector3(0, 45, 0);
                StartCoroutine(snapRotate(rotation));
            }
            if(rightAxis.x < -0.1f && !isRotating)
            {
                Vector3 rotation = new Vector3(0, -45, 0);
                StartCoroutine(snapRotate(rotation));
            }
        }
    }

    void enableJoystickMove(XRController[] controllers, bool state)
    {
        if(state)
        {
            foreach (var controller in controllers)
            {
                controller.moveObjectOut = InputHelpers.Button.PrimaryAxis2DDown;
                controller.moveObjectIn = InputHelpers.Button.PrimaryAxis2DUp;
                controller.rotateObjectLeft = InputHelpers.Button.PrimaryAxis2DLeft;
                controller.rotateObjectRight = InputHelpers.Button.PrimaryAxis2DRight;
            }
        }
        else
        {
            foreach (var controller in controllers)
            {
                controller.moveObjectOut = InputHelpers.Button.None;
                controller.moveObjectIn = InputHelpers.Button.None;
                controller.rotateObjectLeft = InputHelpers.Button.None;
                controller.rotateObjectRight = InputHelpers.Button.None;
            }
        }
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
            Vector3 direction = headYaw * new Vector3(leftAxis.x, 0, leftAxis.y);
            character.Move(direction * Time.fixedDeltaTime * speed);
        }
    }

    void LateUpdate()
    {
        if(shouldTeleport)
        {
            hasTeleported = true;
            shouldTeleport = false;
            character.enabled = false;
            transform.position = teleportPos;
            character.enabled = true;
        }
    }

    IEnumerator snapRotate(Vector3 rotation)
    {
        isRotating = true;
        transform.Rotate(rotation, Space.World);
        yield return new WaitForSeconds(snapRotationCooldown);
        isRotating = false;
    }

    IEnumerator updateHeight(float amount)
    {
        heightIsChanging = true;
        rig.CameraYOffset += amount;
        yield return new WaitForSeconds(heightCooldown);
        heightIsChanging= false;
    }

    public void setActiveLeftHand(bool state)
    {
        leftHandIsActive = state;
    }

    public void setActiveRightHand(bool state)
    {
        rightHandIsActive = state;
    }

    public void setActiveLeftRay(bool state)
    {
        leftRayIsActive = state;
    }

    public void setActiveRightRay(bool state)
    {
        rightRayIsActive = state;
    }

    public void HeightTaller()
    {
        if(!heightIsChanging)
        {
            StartCoroutine(updateHeight(heightChangeAmount));
        }
    }

    public void HeightShorter()
    {
        if(!heightIsChanging)
        {
            float direction = heightChangeAmount *-1;
            StartCoroutine(updateHeight(direction));
        }
    }
    
    public void resetPosition()
    {
        Vector3 dir = transform.position - rig.Camera.transform.position;
        dir = new Vector3(dir.x, 0, dir.z);
        rig.CameraFloorOffsetObject.transform.position += dir;
    }
}
