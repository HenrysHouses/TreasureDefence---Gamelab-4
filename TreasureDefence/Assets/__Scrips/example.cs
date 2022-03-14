using UnityEngine;
using System.Collections.Generic;


public class example : MonoBehaviour
{
    public float t;
    public float value;
    public float speed;
    void Update()
    {
        t += Time.deltaTime * speed;
        value = ExtensionMethods.PingPong(t, -5, 1);
        Vector3 pos = transform.localPosition;
        pos.y = value;
        transform.localPosition = pos;
    }
}
