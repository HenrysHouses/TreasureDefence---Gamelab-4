/*
* Written by:
* Henrik
*/

using UnityEngine;
using TMPro;

public class TowerPlacement_Tutorial : MonoBehaviour
{
    GameObject nextTutorial;
    [SerializeField] Transform raycastPos, heightOffset;
    [SerializeField] TextMeshPro tutorial;
    bool held, wasPlacedCorrectly;

    // Update is called once per frame
    void Update()
    {
        if(wasPlacedCorrectly && !held)
        {
            nextTutorial.SetActive(true);
            Destroy(gameObject);
        }

        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.up);
        Vector3 dir = Camera.main.transform.position - transform.position;
        float offset = Vector3.Dot(forward, dir);

        Vector3 pos = heightOffset.localPosition;
        if(offset > 0.4f)
            pos.y = offset;
        else
            pos.y = 0.4f;

        heightOffset.localPosition = pos;


        if(!held && !wasPlacedCorrectly)
        {
            tutorial.text = "Grab me!";
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(raycastPos.position, Vector3.down, out hit, 2f))
            {		
                if(hit.collider.CompareTag("ValidPlacement"))
                {
                    tutorial.text = "Let go of me!";
                    wasPlacedCorrectly = true;
                }
                else
                {
                    tutorial.text = "Place me on the board!";
                    wasPlacedCorrectly = false;
                }
            }
        }
    }

    public void SetHeld(bool state)
    {
        held = state;
    }
}
