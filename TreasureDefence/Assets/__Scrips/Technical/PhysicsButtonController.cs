using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButtonController : MonoBehaviour
{
    [SerializeField] float threshold = 0.1f;
    [SerializeField] float deadZone = 0.025f;
    
    bool _isPressed;
    Vector3 _startPos;
    ConfigurableJoint _joint;

    public UnityEvent OnPressed, OnReleased;


    // Start is called before the first frame update
    void Awake()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isPressed && GetValue() + threshold >= 1)
            Pressed();
        if(_isPressed && GetValue() - threshold <= 0)
            Released();
    }

    float GetValue()
    {
        float value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;

        if(Mathf.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }

    void Pressed()
    {
        _isPressed = true;
        OnPressed?.Invoke();
        // Debug.Log("pressed");
    }

    void Released()
    {
        _isPressed = false;
        OnReleased?.Invoke();
        // Debug.Log("released");
    }
}
