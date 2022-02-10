/*
 * Written by:
 * Henrik
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WackAMoleController : MonoBehaviour
{
    [SerializeField] MoleController[] moles;
    List<int> activeMoles = new List<int>();
    [SerializeField] bool _playing;
    public bool isPlaying => _playing;
    
    [Header("Arcade Stats")]
    [SerializeField] int simultaneouslyActiveMax;
    [SerializeField] float rangeTrigger, rangeMax;
    [SerializeField] float cooldownRangeMax, cooldownRangeMin;
    bool cooldown;
    public int hitCount;
    [Header("Cost / Rewards")]
    [SerializeField] int WinningHits;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject towerRewardPrefab;
    [SerializeField] int Cost;
    public int ArcadeCost => Cost;  
    // Update is called once per frame
    void Update()
    {
        if(_playing)
        {
            removeInActive();
            
            if(!cooldown)
                StartCoroutine(MolePopUp());

            if(hitCount >= WinningHits)
            {
                Instantiate(towerRewardPrefab, spawnPoint.position, Quaternion.identity);
                Debug.Log("you win");
                _playing = false;
            }
        }
    }

    void removeInActive()
    {
        List<int> remove = new List<int>();
        for (int i = 0; i < activeMoles.Count; i++)
        {
            if(!moles[activeMoles[i]].isMoving || !moles[activeMoles[i]].isHit)
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

    public void StartGame()
    {
        _playing  = true;
        hitCount = 0;
    }
}
