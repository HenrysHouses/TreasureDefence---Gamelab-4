/*
 * Written by:
 * Henrik
*/

using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
	public static WaveController instance;
	
	private void createInstance()
	{
		if(instance == null)
			instance = this;
		else
			Destroy(this);
	}
	
	// Level variables
	[SerializeField] private int health;
	public int currentHealth => health;
	public LevelWaveSequence LevelData;
	private int currentWave;
	Transform EnemyParent;
	public Transform EnemyHolder => EnemyParent;
	
	// Individual wave variables
	bool waveIsInProgress, levelComplete;
	
	public bool waveIsPlaying => waveIsInProgress;
	public bool levelWon => levelComplete;
	
	private int waveProgress;

	// Timer variables
	private float cooldownTimer = 0;
	private float currentCooldown = 0;
	private int repeatSpawn = -1;

	void Start()
	{
		createInstance();
		EnemyParent = GameObject.FindGameObjectWithTag("EnemyHolder").transform;
		GameManager.instance.pathController = LevelData.GetPathController();
		health = LevelData.lives;
	}

	// Update is called once per frame
	void  Update()
	{
		if(health <= 0)
			endLevel(true);
			
		if(waveIsInProgress)
		{
			
			if(waveProgress == getWaveLength())
			{
				waveIsInProgress = false;
				EndOfWave();
			}
			
			if(cooldownTimer > currentCooldown)
			{
				EnemyBehaviour spawn = SpawnNextEnemy();
				
				if(spawn)
					GameManager.instance.enemies.Add(spawn);
			}
			else
				cooldownTimer += Time.deltaTime;
		}		
	}
	public void nextWave()
	{
		if(!levelComplete)
			waveIsInProgress = true;
		else
			endLevel();
	}
	
	public void endLevel(bool lose = false)
	{
		GetComponentInParent<LevelHandler>().ExitLevel();
		foreach (var enemy in GameManager.instance.enemies)
		{
			Destroy(enemy.gameObject);
		}
		GameManager.instance.enemies = new List<EnemyBehaviour>();
		Debug.Log("Level is complete");
		Debug.LogError("end of level is not implemented, see: " + this);
	}
	
	public void	dealDamage(int damage)
	{
		health -= damage;
	}
	
	public void	heal(int life)
	{
		health += life;
	}
	
	private int getWaveLength()
	{
		return LevelData.waves[currentWave].waveData.Length;
	}
	
	private int getWaveCount()
	{
		return LevelData.waves.Length;
	}
	
	private void EndOfWave()
	{
		currentWave++;
		waveProgress = 0;
		cooldownTimer = 0;
		currentCooldown = 0;
		repeatSpawn = -1;
		
		if(currentWave == getWaveCount())
			levelComplete = true;
	}
	
	EnemyBehaviour SpawnNextEnemy()
	{
		GameObject spawn = null;
		
		// Debug.Log("wave progress");
		if(repeatSpawn == -1)
		{
			repeatSpawn = getRepeatSpawn();
			// Debug.Log("new repeat spawn");
		}
		else if(repeatSpawn > -1)
		{
			// update repeat spawning
			repeatSpawn--;
			
			// Spawn the next enemy
			spawn = Instantiate(getEnemyPrefab());
			spawn.transform.SetParent(EnemyParent, true);
			
			currentCooldown = getCooldown();
			cooldownTimer = 0;
			
			// update wave progress		
			if(repeatSpawn == -1)
				waveProgress++;
			// Debug.Log("decrease spawn repeat");
		}
		if(spawn != null)
			return spawn.GetComponent<EnemyBehaviour>();
		// Debug.Log(" no enemy was spawned");
		return null;
	}
	
	GameObject getEnemyPrefab()
	{
		return LevelData.waves[currentWave].waveData[waveProgress].enemyPfb;
	}
	
	private float getCooldown()
	{
		return LevelData.waves[currentWave].waveData[waveProgress].Cooldown;
	}
	
	private int getRepeatSpawn()
	{
		int n = Mathf.Clamp(LevelData.waves[currentWave].waveData[waveProgress].repeat -1, 0, 100000);
		return n;	
	}
}