using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	[HideInInspector] public MeshRenderer LevelPickupHighlight, WaterHighlight;
	[SerializeField] Transform TowerParent;

	[Header("Level Specifics")]
	public PathController pathController;
	[SerializeField] private List<GameObject> towers = new List<GameObject>();
	[SerializeField] private List<GameObject> TowersAtLevelStart = new List<GameObject>();
	private int playerHealth;
	
	[Header("Lighting Controls")]
	public LightController[] RealtimeLights;
	[SerializeField] TowerDispenserController towerDispenser;

	public void setLights(bool state)
	{
		foreach (var light in RealtimeLights)	
		{
			light.fadeLightSate = state;
		}
	}
	
	public float healthMultiplier = 1f;
	public float moneyMultiplier = 1f;
	public float rangeMultiplier = 1f;
	public float damageMultiplier = 1f;
	public float attSpeedMultiplier = 1f;
	

	// debugging update
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha0))
		{
			Time.timeScale = 0.3f;
			Debug.Log("time is slowed");
		}

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			Time.timeScale = 1;
			Debug.Log("time is normal");
		}
	}


	public void StoreCurrentTowers()
	{
		foreach(GameObject tower in towers)
		{
			TowersAtLevelStart.Add(tower);
		}

		for (int i = 0; i < TowersAtLevelStart.Count; i++)
		{
			if(TowersAtLevelStart[i] == null)
			{
				TowersAtLevelStart.RemoveAt(i);
			}
		}
		Debug.Log("stored");
	}

	public void resetTowerDispenser()
	{
		towerDispenser.fillDispenser(towers);
	}

	public void restorePreLevelTowers()
	{
		foreach(GameObject invalid in towers)
		{
			if(!TowersAtLevelStart.Contains(invalid))
			{
				towers.Remove(invalid);
				Destroy(invalid);
			}
			else
			{
			}
		}
		resetTowerDispenser();
	}

	public void AddTowerToList(GameObject tower)
	{
		towers.Add(tower);
	}

	public void RemoveActiveTowers()
	{
		foreach (var tower in towers)
		{
			Destroy(tower);
		}
		
		towers = new List<GameObject>();
	}

	public void SpawnTower(GameObject tower, Vector3 position)
	{
		GameObject spawnedTower = Instantiate(tower, position, Quaternion.identity);
		towers.Add(spawnedTower);
		spawnedTower.transform.SetParent(TowerParent, true);
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
