using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerendererHighlighter : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform targetPos;
    [SerializeField] Transform targetHeight;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, targetPos.position);
        lineRenderer.SetPosition(1, targetHeight.position);
    }
}
