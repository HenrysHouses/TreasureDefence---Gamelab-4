using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleOnEnd : MonoBehaviour
{
	public ParticleSystem[] particles;
	public float[] startLifetime;
	public bool overrideUpdate;

	void Start()
	{
		foreach (var vfx in particles)
		{
			float totalDuration = vfx.main.duration + vfx.startLifetime;
			Debug.Log(totalDuration);
			Destroy(vfx.gameObject, totalDuration);
		}
	}
	
	void Update()
	{
		if(transform.childCount == 0 && !overrideUpdate)
		{
			Destroy(gameObject);
		}
	}
}
