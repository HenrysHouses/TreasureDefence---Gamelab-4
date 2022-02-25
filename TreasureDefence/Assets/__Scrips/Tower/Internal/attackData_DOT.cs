using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackData_DOT : attackData
{
	public float Duration, DamageInterval, DamageTimer;

	[Tooltip("This is when Duration <= 0")]
	public bool DurationHasEnded, DealDamage;
	override public Vector3 UpdateProjectile()
	{
		if (target)
		{	
			if (!hit)
            {

				t = t + Time.deltaTime * projectileSpeed;			 //T is a value = 0. The closer to 1 or more than 1 it is a hit.
				t = Mathf.Clamp(t, 0, 1);							 
				if (t >= 1)
				{
					hit = true;
					return target.position;
				}
				return Vector3.Slerp(startPos, target.position, t);
			}
			else
            {   //DOT


				if (Duration > 0)
                {
					Duration -= Time.deltaTime;
					DamageTimer += Time.deltaTime;

					if (DamageTimer > DamageInterval)
                    {
						Deal_DOTDamage();
                    }

                }
				else
                {
					DurationHasEnded = true;
					DamageTimer = 0;
                }


            }

		}
		return transform.position;
	}

	void Deal_DOTDamage()
    {
		enemy.TakeDamage(1);
    }

}
