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
		Debug.LogWarning("GetPathOP does not result in even enemy movement speed. use GetEvenPathOP instead");
	}

	void Update()
	{
		EnemyUpdate();
		progress = Mathf.Clamp(progress + Time.deltaTime * enemyInfo.speed, 0, 1);
		
		op = path.GetPathOP(progress);// ! use GetEvenPathOP

		transform.localPosition = op.pos;
		transform.rotation = op.rot;
		
		if(progress >= 1 && WaveController.instance.currentHealth > 0) // when the enemy reaches the end
		{
			if(!isAttacking)
				StartCoroutine(Attack());
		}
	}

	/// <summary>Deals damage to this enemy</summary>
	/// <param name="damageAmount">int amount of damage to deal</param>
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
		WaveController.instance.RemoveEnemy(this);
		// CurrencyManager.instance.AddMoney(enemyInfo.moneyReward);
		Debug.LogWarning("adding money is currently disabled");
		DeathRattle();
		Destroy(gameObject);    // Let's improve this at some point
	}
	
	/// <summary>Called when the enemy reaches the end of the path</summary>
	public virtual IEnumerator Attack()
	{
		isAttacking = true;
		WaveController.instance.dealDamage(enemyInfo.damage);
		// play attack anim?
		yield return new WaitForSeconds(enemyInfo.attackCooldown);
		isAttacking = false;
	}
	
	/// <summary>Called when the enemy dies</summary>
	public virtual void DeathRattle(){}
	
	/// <summary>Called at the beginning of the Update</summary>
	public abstract void EnemyUpdate();
	
	/// <summary>Called when the enemy takes damage</summary>
	public virtual void DamageTrigger(){}
	
	/// <summary>Called when an attack is triggered</summary>	
	public abstract void AnimTrigger();
}