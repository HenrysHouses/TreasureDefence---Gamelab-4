using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
public class VR_ContinousMovement : MonoBehaviour
{
    public float speed, turnSpeed;
    public XRNode movementNode, rotationNode;
    Vector2 moveInput, rotateInput;
    bool rotateOverride, canRotate = true;
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
        InputDevice device = InputDevices.GetDeviceAtXRNode(movementNode);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out moveInput);
        device.TryGetFeatureValue(CommonUsages.primaryButton, out rotateOverride);
        device = InputDevices.GetDeviceAtXRNode(rotationNode);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out rotateInput);    
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(moveInput.x, 0, moveInput.y);

        character.Move(direction * Time.fixedDeltaTime * speed);
        
        if(canRotate || rotateOverride)
        {
            Vector3 rotation = new Vector3(0, rotateInput.x, 0);
            transform.Rotate(rotation, Space.World);
        }
    }

    public void setCanRotate(bool state)
    {
        canRotate = state;
    }
}
