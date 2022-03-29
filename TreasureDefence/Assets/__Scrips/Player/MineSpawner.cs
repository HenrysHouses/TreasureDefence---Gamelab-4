using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    public GameObject Mine;
    public static MineSpawner Instance;
    public Transform SpawnPosition;
    public List<GameObject> MineList = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.M))
        {
            InstantiateMine(1);
        }
    }

    public void InstantiateMine(int Number)
    {
        for(int i = 0; i < Number; i++)
        {
            MineList.Add(Instantiate(Mine, SpawnPosition.position, SpawnPosition.rotation));
        }

    }

    public void DestroyAllMines()
    {
        for (int i = 0; i < MineList.Count; i++)
        {
            Destroy(MineList[i]);
            MineList.RemoveAt(i);
        }
    }
}
