using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    bool isHolding;   //Not used
    public Material highlightMaterial, defaultMaterial;
    public string selectableTag;
    public Transform _selection;

    //Empty where the held object will have its position set to.
    public Transform ObjectHolder;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         if (isHolding)
         {

         }



        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {

                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }
                _selection = selection;
            }

        }

        if (Physics.Raycast(ray, out hit) && Input.GetMouseButton(0))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selection.transform.position = ObjectHolder.transform.position;      //For some odd reason this _Selection falls at random times?
            }
        }


    }

   


}
