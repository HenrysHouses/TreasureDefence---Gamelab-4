using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleGameController : MonoBehaviour
{
    // timer var
    // Insert TextMesh text
    public float gameTimer = 30f;

    public GameObject MoleContainer;
    private Mole[] moles;
    public float ShowMoleTimer = 1.5f;
    float resetMoleTimer;

    // Start is called before the first frame update
    void Start()
    {
        // mole equals all Children in MoleContainer object that has "Mole" component on them
        moles = MoleContainer.GetComponentsInChildren<Mole>();
        resetMoleTimer = ShowMoleTimer;
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer -= Time.deltaTime;

        if (gameTimer > 0f)
        {
            ShowMoleTimer -= Time.deltaTime;
            if (ShowMoleTimer < 0f)
            {
                //Debug.Log("Test inside showmole timer");

                // Call ShowMole method from on of the moles in mole container
                moles[Random.Range(0, moles.Length)].ShowMole();

                ShowMoleTimer = resetMoleTimer;
            }
        }
    }
}
