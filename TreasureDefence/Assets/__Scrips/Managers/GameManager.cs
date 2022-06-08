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
	[SerializeField] public SaveData SaveData;

	private int playerHealth;
	
	[Header("Lighting Controls")]
	[SerializeField] bool TimeState = true;
	public LightController[] RealtimeLights;
	[SerializeField] TowerDispenserController towerDispenser;
	[SerializeField] MeshRenderer skybox;
	[SerializeField] Color NightFogColor, DayFogColor;
	[SerializeField] Light directionalLight;
	[SerializeField] float TimeSpeed = 1;
	float timeOfDay;

	public void setLights(bool state)
	{
		TimeState = state;
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
	
	void Start()
	{
		Debug.Log("Slow motion Cheats active, buttons Alpha 0, and 1");
		Debug.Log("Unlock levels Cheats active, buttons Alpha 7");
	}

	// debugging update
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha0))
		{
			setLights(true);
			Debug.Log("time is Day");
		}

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			setLights(false);
			Debug.Log("time is night");
		}

		if(Input.GetKeyDown(KeyCode.Alpha7))
		{
			SaveLevelComplete(0);
			SaveLevelComplete(1);
			SaveLevelComplete(2);
			Debug.Log("unlocked all levels");
		}



		if(!TimeState)
			timeOfDay = Mathf.Clamp01(timeOfDay + Time.deltaTime * TimeSpeed);
		else
			timeOfDay = Mathf.Clamp01(timeOfDay - Time.deltaTime * TimeSpeed);
		directionalLight.color = Vector4.Lerp(Color.white, Color.black, timeOfDay);
		skybox.material.SetFloat("_TimeOfDay", timeOfDay);
		RenderSettings.fogColor = Color.Lerp(DayFogColor, NightFogColor, timeOfDay);
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
		// Debug.Log("stored");
	}

	public void resetTowerDispenser()
	{
		towerDispenser.fillDispenser(towers);
	}

	public void restorePreLevelTowers()
	{
		List<GameObject> InvalidTowers = new List<GameObject>();
		foreach(GameObject invalid in towers)
		{
			if(!TowersAtLevelStart.Contains(invalid))
			{
				InvalidTowers.Add(invalid);
			}
		}
		foreach(GameObject tower in InvalidTowers)
		{
			towers.Remove(tower);
			Destroy(tower);
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

	public void RemoveTower(GameObject Tower)
	{
		towers.Remove(Tower);
	}

	public void SpawnTower(GameObject tower, Vector3 GlobalPosition)
	{
		GameObject spawnedTower = Instantiate(tower, GlobalPosition, Quaternion.identity);
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

	public void SaveLevelComplete(int level)
	{
		for (int i = 0; i < level; i++)
		{
			// Debug.Log(i);
			if(SaveData.LevelCompletionStates[i] == false)
				return;
		}
		// Debug.Log("saved");
		SaveData.LevelCompletionStates[level] = true;
	}

	void enableNextLevel()
	{
		
	}
}

[System.Serializable]
public struct SaveData
{
	public bool[] LevelCompletionStates;
}
