using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AngularPhysicsButtonController : MonoBehaviour
{
    [SerializeField] float threshold = 0.1f;
    [SerializeField] float deadZone = 0.025f;
    [SerializeField] float linearLimit = 0.03f;
    [SerializeField] float springForce = 30;
    Rigidbody rb;
    [SerializeField] Transform ButtonRestingPos;
    bool _isPressed;
    Vector3 _startPos;
    public UnityEvent OnPressed, OnReleased;

    float dot;

    void Awake()
    {
        _startPos = transform.localPosition;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spring physics
        Vector3 up = transform.TransformDirection(Vector3.up);
        Vector3 dir = transform.position - ButtonRestingPos.position;
        dot = Vector3.Dot(up, dir);

        if(dot > 0.001 || dot < -0.001)
            rb.AddForce(transform.up * dot *-1 * springForce);

        // Constrain X movement
        Vector3 X = transform.TransformDirection(Vector3.right);
        if(Vector3.Dot(X, dir) > 0.001 || Vector3.Dot(X, dir) < -0.001)
            rb.velocity = transform.right * Vector3.Dot(X, dir) * -1 * 10;
        
        // Constrain Y movement
        Vector3 Y = transform.TransformDirection(Vector3.forward);
        if(Vector3.Dot(Y, dir) > 0.001 || Vector3.Dot(Y, dir) < -0.001)
            rb.velocity = transform.forward * Vector3.Dot(Y, dir) * -1 * 10;
        
        // Checks state
        if(!_isPressed && GetValue() + threshold >= 1)
            Pressed();
        if(_isPressed && GetValue() - threshold <= 0)
            Released();
    }

    float GetValue()
    {
        float value = dot / linearLimit * -1;

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
