using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastForInteractable : MonoBehaviour
{
    public string tag;

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.5f))
        {
            tag = hit.transform.tag;
            
            Invoke(("" + tag), 0f);
        }
    }

    void Cup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.StartWave();
        }
    }

    void Tower()
    {
        // Example
        // TowerInfoUI.SetActive(true);
    }
    
    void Enemy()
    {
        // Example
        // EnemyInfoUI.SetActive(true);
    }

    void Untagged()
    {
        
    }

    void RayPick()
    {
        
    }
    
}
