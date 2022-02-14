using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;


	public int[] enemyPerWave;

	
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
	}
}
