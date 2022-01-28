using UnityEngine;

[CreateAssetMenu(menuName = "TreasureDefence/New Level")]
public class LevelWaveSequence : ScriptableObject
{
	public GameObject LevelPrefab;
	public WaveSequence[] waves;
	
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
	public float Cooldown;
	public int repeat;
}
