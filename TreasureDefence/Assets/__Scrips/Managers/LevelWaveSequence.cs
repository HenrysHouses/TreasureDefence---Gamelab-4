using UnityEngine;

[CreateAssetMenu(menuName = "TreasureDefence/New Level")]
public class LevelWaveSequence : ScriptableObject
{
	public int levelNumber;
	public GameObject LevelPrefab;
	public WaveSequence[] waves;
	
	public int lives = 1;
	public int StartMoney = 20;

	public PathController GetPathController()
	{
		return LevelPrefab.GetComponentInChildren<PathController>();
	}
}


[System.Serializable]
public struct WaveSequence
{
	public waveEnemyData[] waveData;
}


[System.Serializable]
public struct waveEnemyData
{
	public GameObject enemyPfb;
	public int repeat;
	public float Cooldown;
}
