using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WackAMoleController : MonoBehaviour
{
    [SerializeField] List<MoleData> moles = new List<MoleData>();
    float random;
    [SerializeField] float rangeTrigger, rangeMax;

    // Update is called once per frame
    void Update()
    {
        random = Random.Range(0, rangeMax);

        if(random > rangeTrigger)
            moles[0].showMole();
    }
}
