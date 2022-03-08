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
	private int playerHealth;
	
	[Header("Lighting Controls")]
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
