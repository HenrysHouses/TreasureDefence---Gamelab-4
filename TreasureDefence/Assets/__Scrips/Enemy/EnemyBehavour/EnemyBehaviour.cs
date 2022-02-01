/*
 * Written by:
 * Henrik
*/

using UnityEngine;

abstract public class EnemyBehaviour : MonoBehaviour
{
	public PathController path;
	public SO_Test enemyStats;
	private OrientedPoint op;
	[SerializeField] int health;
	[SerializeField] int moneyReward;
		
	//public EnemyScriptableObject eso;

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
		
		progress = Mathf.Clamp(progress + Time.deltaTime * enemyStats.speed, 0, 1);
		
		op = path.GetPathOP(progress);// ! use GetEvenPathOP

		transform.localPosition = op.pos;
		transform.rotation = op.rot;
	}

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
		CurrencyManager.instance.MoneyToAdd(moneyReward);
		DeathRattle();
		Destroy(gameObject);    // Let's improve this at some point
	}
	
	/// <summary>called when the enemy dies</summary>
	public abstract void DeathRattle();
	public abstract void EnemyUpdate();
	public virtual void DamageTrigger()
	{
		// spawn particles here
	}
}