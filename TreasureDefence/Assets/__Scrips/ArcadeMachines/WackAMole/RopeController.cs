using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public LineRenderer rope;

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    // Start is called before the first frame update
    void Start()
    {
        rope.positionCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        rope.SetPosition(0, pos1.position);
        rope.SetPosition(1, pos2.position);
        rope.SetPosition(2, pos3.position);
    }
}
