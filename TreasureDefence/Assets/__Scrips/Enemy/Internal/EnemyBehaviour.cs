/*
 * Written by:
 * Henrik - Used old code written by Rune
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
// using TMPro;

[RequireComponent(typeof(StudioEventEmitter))]
abstract public class EnemyBehaviour : MonoBehaviour
{
	StudioEventEmitter _AudioSource;
	[SerializeField] GameObject DeathSound;
	public WaveController waveController;
	public PathController path;
	public EnemyInfo enemyInfo;
	private OrientedPoint op;
	int health;
	public int GetHealth => health;
	bool isAttacking;
	/// <summary>should always be minimun length of attack anim ! This may be changed to automatic anim length</summary>
	MeshRenderer mr;
	[SerializeField] float progress;
	public List<modifier> speedMod = new List<modifier>();
	public List<EnemyDebuff> Debuffs = new List<EnemyDebuff>();
	public string[] _CurrentDebuffs;
	
	// private float health;
	// private float armor;
	// private float speed;
	// private int priority;
	// private Mesh[] meshes;

	public void Start()
	{
		waveController = GameManager.instance.GetWaveController();
		mr = GetComponent<MeshRenderer>();
		path = GameManager.instance.pathController;
		_AudioSource = GetComponent<StudioEventEmitter>();
		health = enemyInfo.health;

		// Debug.Log("Enemy Start " + gameObject.name);
	}

	void Update()
	{
		// temp
		_CurrentDebuffs = new string[Debuffs.Count];
		for (int i = 0; i < Debuffs.Count; i++)
		{
			_CurrentDebuffs[i] = Debuffs[i].GetType().ToString();
		}

		// Handling debuffs
		List<EnemyDebuff> expiredDebuffs = new List<EnemyDebuff>();
		foreach(EnemyDebuff effect in Debuffs)
		{
			if(!effect.Update(this))
			{
				expiredDebuffs.Add(effect);
			}
		}
		foreach (EnemyDebuff remove in expiredDebuffs)
		{
			Debuffs.Remove(remove);
		}

		// handling enemy behaviour
		EnemyUpdate();
		float speedEffects = SumOfModifiers(speedMod);
		float pathLength = path.GetApproxLength();
		float finalSpeed = enemyInfo.speed * speedEffects;
		finalSpeed = (100 / pathLength) * finalSpeed;
		progress = Mathf.Clamp(progress + Time.deltaTime * finalSpeed, 0, 1);
		
		op = path.GetEvenPathOP(progress);

		transform.localPosition = op.pos;
		transform.rotation = path.GetPathOP(progress).rot;
		
		if(progress >= 1 && waveController.currentHealth > 0) // when the enemy reaches the end
		{
			if(!isAttacking)
				StartCoroutine(Attack());
		}
	}


	float SumOfModifiers(List<modifier> modifiers)
	{
		float sum = 1;
		foreach (var mod in modifiers)
		{
			switch(mod.type)
			{
				case modifierType.multiplicative:
					sum *= mod.value;
					break;

				case modifierType.additiveFlat:
					sum+= mod.value;
					break;

				case modifierType.additivePercentage:
					sum += (sum/100*mod.value);
					break;
			}
		}
		return sum;
	}



	/// <summary>Deals damage to this enemy</summary>
	/// <param name="damageAmount">int amount of damage to deal</param>
	public void TakeDamage(int damageAmount)
	{
		// Debug.Log("Health: " + health);
		
		DamageTrigger();
		health -= damageAmount;

		
		
		// mr.material.color = Color.red;
		
		// Invoke(nameof(ResetColor), 0.1f);
		
		if (health <= 0)
		{
			DeathTrigger();
		}
		else
			_AudioSource.Play();
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
		Instantiate(DeathSound, transform.position, Quaternion.identity);
		waveController.RemoveEnemy(this);
		CurrencyManager.instance.AddMoney(enemyInfo.moneyReward);
		// Debug.LogWarning("adding money is currently disabled");

		PirateCoveController.instance.AddExperience(enemyInfo.experienceReward);
		
		DeathRattle();
		foreach (var effect in Debuffs)
		{
			effect.ExpirationTrigger(this);
		}
		Destroy(gameObject);    // Let's improve this at some point
	}
	
	/// <summary>Called when the enemy reaches the end of the path</summary>
	public virtual IEnumerator Attack()
	{
		// Debug.Log("attack " + gameObject.name);
		isAttacking = true;
		waveController.dealDamage(enemyInfo.damage);
		// play attack anim?
		yield return new WaitForSeconds(enemyInfo.attackCooldown);
		isAttacking = false;
	}

	void OnDestroy()
	{
		foreach(var effect in Debuffs)
		{
			if(effect.gameObject)
				Destroy(effect.gameObject);
		}
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

[System.Serializable]
public struct modifier
{
	public float value;
	public modifierType type;
}

public enum modifierType
{
	multiplicative,
	additivePercentage,
	additiveFlat
}