using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WackAMoleController : MonoBehaviour
{
    [SerializeField] MoleController[] moles;
    List<int> activeMoles = new List<int>();
    
    [Header("Arcade Stats")]
    [SerializeField] int simultaneouslyActiveMax;
    [SerializeField] float rangeTrigger, rangeMax;
    [SerializeField] float cooldownRangeMax, cooldownRangeMin;
    [SerializeField] bool cooldown;
    public int hitCount;
    [SerializeField] int WinningHits;
    // Update is called once per frame
    void Update()
    {
        removeInActive();
        
        if(!cooldown)
            StartCoroutine(MolePopUp());
    }

    void removeInActive()
    {
        List<int> remove = new List<int>();
        for (int i = 0; i < activeMoles.Count; i++)
        {
            if(!moles[activeMoles[i]].isMoving)
            {
                remove.Add(activeMoles[i]);
            }
        }
        foreach (var item in remove)
            activeMoles.Remove(item);
    }

    IEnumerator MolePopUp()
    {
        cooldown = true;
        float randomCooldown = Random.Range(cooldownRangeMin, cooldownRangeMax);
        float randomTrigger = Random.Range(0, rangeMax);
        if(randomTrigger > rangeTrigger && simultaneouslyActiveMax > activeMoles.Count)
        {
            int selected = Random.Range(0, moles.Length); 
            if(!moles[selected].isMoving)
            {
                activeMoles.Add(selected);
                moles[selected].showMole();
            }
        }
        yield return new WaitForSeconds(randomCooldown);
        cooldown = false;
    }
}
