using UnityEngine;

using UnityEngine;

[CreateAssetMenu(menuName = "TreasureDefence/LevelWaveSequence")]
public class LevelWaveSequence : ScriptableObject
{
	public WaveSequence[] waves;
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
