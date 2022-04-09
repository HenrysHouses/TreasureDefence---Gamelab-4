// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// //Henrik & Mikkel.
// [System.Serializable]
// public class attackData_DOT : attackData
// {
// 	public float Duration, DamageInterval, DamageTimer;

// 	[Tooltip("This is when Duration <= 0")]
// 	public bool DurationHasEnded, DealDamage;
// 	override public Vector3 UpdateProjectile()
// 	{
// 		foreach (EnemyBehaviour priority in enemyPriority)
//         {
//             if(priority)
//             {
// 				CurrentTarget = priority;

// 				if (!hit)
// 				{

// 					t = t + Time.deltaTime * projectileSpeed;			 //T is a value = 0. The closer to 1 or more than 1 it is a hit.
// 					t = Mathf.Clamp(t, 0, 1);							 
// 					if (t >= 1)
// 					{
// 						hit = true;
// 						return CurrentTarget.transform.position;
// 					}
// 					return Vector3.Slerp(startPos, CurrentTarget.transform.position, t);
// 				}
// 				else
// 				{   //DOT


// 					if (Duration > 0)
// 					{
// 						Duration -= Time.deltaTime;
// 						DamageTimer += Time.deltaTime;

// 						if (DamageTimer > DamageInterval)
// 						{
// 							Deal_DOTDamage();
// 						}

// 					}
// 					else
// 					{
// 						DurationHasEnded = true;
// 						DamageTimer = 0;
// 					}


// 				}
// 			}
// 		}
// 		CurrentTarget = null;
// 		return transform.position;
// 	}

// 	void Deal_DOTDamage()
//     {
// 		CurrentTarget.TakeDamage(1);
//     }

// }
