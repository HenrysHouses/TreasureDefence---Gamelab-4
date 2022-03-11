using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_TeleportationAreaHighlighter : MonoBehaviour
{
    [SerializeField] GameObject highlight;
    Renderer highlightRenderer;
    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        highlightRenderer = highlight.GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(line)
        {
            Vector3 floorPos = new Vector3();
            if(line.positionCount > 1)
                floorPos = line.GetPosition(line.positionCount-1);

            highlight.transform.position = floorPos;
            highlight.transform.rotation = Quaternion.identity;
            highlightRenderer.material.color = line.colorGradient.colorKeys[0].color;
            highlightRenderer.material.SetColor("_Color2", line.colorGradient.colorKeys[0].color);
        }        
    }
}
