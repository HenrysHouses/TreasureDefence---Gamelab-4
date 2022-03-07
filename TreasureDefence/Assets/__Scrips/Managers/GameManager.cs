using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[SerializeField] private List<GameObject> towers = new List<GameObject>();
	
	public GameObject LevelDisplayHighlight, LevelHolderHighlight;
	public int[] enemyPerWave;

	public MeshRenderer LevelPickupHighlight, WaterHighlight;

	public PathController pathController;
	private int playerHealth;
	
	
	public LightController[] RealtimeLights;
	public void setLights(bool state)
	{
		foreach (var light in RealtimeLights)	
		{
			light.fadeLightSate = state;
		}
	}

	public void AddTowerToList(GameObject tower)
	{
		towers.Add(tower);
	}

	public void RemoveTowersOnLevelEnd()
	{
		for (int i = 0; i < towers.Count; i++)
		{
			Destroy(towers[i]);
		}
		
		towers = new List<GameObject>();
	}

	private WaveController waveController;
	public WaveController GetWaveController()
	{
		if(waveController == null)
		{
			GameObject _level = GameObject.FindGameObjectWithTag("CurrentLevel");
			if(_level)
				waveController = _level.GetComponent<WaveController>(); // this will need to be changed later
		}
		return waveController;
	}

	
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		
		Cursor.lockState = CursorLockMode.Locked;

		LevelPickupHighlight = GameObject.FindGameObjectWithTag("Highlight/LevelPickup").GetComponent<MeshRenderer>();
		WaterHighlight = GameObject.FindGameObjectWithTag("Highlight/Water").GetComponent<MeshRenderer>();
	}
}
