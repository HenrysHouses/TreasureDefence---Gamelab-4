using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public PathController path;
	
	public EnemyInfo enemyInfo;

	private OrientedPoint op;

	[SerializeField] int health;
	
	[SerializeField] float scaleTime;
	
	//public EnemyScriptableObject eso;

	MeshRenderer mr;
	
	public float progress;
	
	// private float health;
	// private float armor;
	// private float speed;
	// private int priority;
	// private Mesh[] meshes;

	private void Start()
	{
		mr = GetComponent<MeshRenderer>();
		
		// transform.localScale = Vector3.zero;
		
		path = GameManager.instance.pathController;

		// InvokeRepeating(nameof(InitialScale), 0f, Time.fixedDeltaTime);
	}

	void Update()
	{
		
		progress = Mathf.Clamp(progress + Time.deltaTime * enemyInfo.speed, 0, 1);
		
		op = path.GetEvenPathOP(progress);
		// op = path.GetPathOP(enemyStats.speed * progress);

		transform.localPosition = op.pos;
		transform.rotation = op.rot;
	}



	private void InitialScale()
	{
		// transform.localScale += Vector3.one * scaleTime * Time.fixedDeltaTime;
		
		
		if (transform.localScale.x > 0.099f)
		{
			transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			CancelInvoke();
		}
	}

	public void TakeDamage(int damageAmount)
	{
		health -= damageAmount;

		mr.material.color = Color.red;
		
		Invoke(nameof(ResetColor), 0.1f);
		
		if (health <= 0)
		{
			Destroy(gameObject);    // Let's improve this at some point
		}
	}

	private void ResetColor()
	{
		mr.material.color = Color.white;
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}
	
	public float GetProgress()
	{
		return progress;
	}
	
	private void OnDestroy()
	{
		// GameManager.instance.RemoveEnemy(this);
	}
}


