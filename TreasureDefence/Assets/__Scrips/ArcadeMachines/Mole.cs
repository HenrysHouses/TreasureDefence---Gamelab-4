using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    /*
    [Tooltip("Insert Y position when the object should be visible")]
    public float VisibleYHeight = 1.75f; 
    
    [Tooltip("Insert Y position when the object should be hidden")]
    public float HiddenYHeight =  0.75f;
    */

    public Transform showPos, hidePos;
    // Try using Transforms instead and see if that works




    private Vector3 myNewXYZPosition;

    public float MoleSpeed = 4f;

    private void Awake()
    {
        HideMole();

        transform.position = myNewXYZPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move mole
        transform.position = Vector3.Lerp(hidePos.position, myNewXYZPosition, Time.deltaTime * MoleSpeed);
    }
    public void HideMole()
    {
        transform.localPosition = new Vector3(
            transform.position.x, hidePos.position.y, transform.position.z);
    }

    public void ShowMole()
    {
        //Debug.Log("inside show mole");
        myNewXYZPosition = new Vector3(
            transform.position.x, showPos.position.y, transform.position.z);
    }
}
