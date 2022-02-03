/*
 * Written by:
 * Henrik - Used old code written by Rune
*/
using System.Collections;

using UnityEngine;

abstract public class EnemyBehaviour : MonoBehaviour
{
	public PathController path;
	public EnemyInfo enemyInfo;
	private OrientedPoint op;
	[SerializeField] int health;
	bool isAttacking;
	/// <summary>should always be minimun length of attack anim ! This may be changed to automatic anim length</summary>
	MeshRenderer mr;
	
	float progress;
	
	// private float health;
	// private float armor;
	// private float speed;
	// private int priority;
	// private Mesh[] meshes;

	public void Start()
	{
		mr = GetComponent<MeshRenderer>();
		path = GameManager.instance.pathController;
	}

	void Update()
	{
		
		progress = Mathf.Clamp(progress + Time.deltaTime * enemyInfo.speed, 0, 1);
		
		op = path.GetPathOP(progress);// ! use GetEvenPathOP
		Debug.LogWarning("GetPathOP does not result in even enemy movement speed. use GetEvenPathOP instead");

		transform.localPosition = op.pos;
		transform.rotation = op.rot;
		
		if(progress >= 1 && WaveController.instance.currentHealth > 0) // when the enemy reaches the end
		{
			if(!isAttacking)
				StartCoroutine(Attack());
		}
	}

	public virtual IEnumerator Attack()
	{
		isAttacking = true;
		WaveController.instance.dealDamage(enemyInfo.damage);
		// play attack anim?
		yield return new WaitForSeconds(enemyInfo.attackCooldown);
		isAttacking = false;
	}
	
	public abstract void AnimTrigger();

	public void TakeDamage(int damageAmount)
	{
		DamageTrigger();
		health -= damageAmount;

		// mr.material.color = Color.red;
		
		// Invoke(nameof(ResetColor), 0.1f);
		
		if (health <= 0)
		{
			DeathTrigger();
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
	
	public float GetProgress
	{
		set => progress = value;
		get => progress;
	}
	
	private void DeathTrigger()
	{
		GameManager.instance.RemoveEnemy(this);
		// CurrencyManager.instance.AddMoney(enemyInfo.moneyReward);
		DeathRattle();
		Destroy(gameObject);    // Let's improve this at some point
	}
	
	/// <summary>called when the enemy dies</summary>
	public virtual void DeathRattle(){}
	public abstract void EnemyUpdate();
	public virtual void DamageTrigger()
	{
		// spawn particles here
	}
}