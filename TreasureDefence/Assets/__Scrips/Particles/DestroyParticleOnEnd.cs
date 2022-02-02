using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleOnEnd : MonoBehaviour
{
	public ParticleSystem[] particles;

	void Start()
	{
		foreach (var vfx in particles)
		{
			float totalDuration = vfx.main.duration + vfx.startLifetime;
			Destroy(vfx.gameObject, totalDuration);
		}
	}
	
	void Update()
	{
		if(transform.childCount == 0)
			Destroy(gameObject);
	}
}
