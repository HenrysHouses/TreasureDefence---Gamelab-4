/*
 * Written by:
 * Henrik
*/
using UnityEngine;

public class attackData
{
	public GameObject gameObject;
	public Transform transform;
	public EnemyBehaviour[] enemyPriority;
    public EnemyBehaviour CurrentTarget;
	public Vector3 startPos;
	public float projectileSpeed;
	public int projectileDamage;
	public float t;
	public bool hit, curve;

	virtual public Vector3 UpdateProjectile()
    {

        foreach (EnemyBehaviour priority in enemyPriority)
        {
            if(priority)
            {
                if(CurrentTarget != priority)
                    // Debug.Log("New target: " + priority.name);
                CurrentTarget = priority;

                if (curve)
                {
                    t = t + Time.deltaTime * projectileSpeed;
                    t = Mathf.Clamp(t, 0, 1);
                    if(t >= 1)
                    {
                        hit = true;
                        return priority.transform.position;
                    }
                    return Vector3.Slerp(startPos, priority.transform.position, t);
                }
                
                t = t + Time.deltaTime * projectileSpeed;
                t = Mathf.Clamp(t, 0, 1);
                if(t >= 1)
                {
                    hit = true;
                    return priority.transform.position;
                }
                return Vector3.Lerp(startPos, priority.transform.position, t);
            }    
        }
        CurrentTarget = null;        
        return transform.position;
    }

	public void print()
	{
		Debug.Log("GameObject: " + gameObject + "\n Transform: " + transform + "\n startPos: " + startPos + "\n projectileSpeed: " + projectileSpeed + "\n projectileDamage: " + projectileDamage + "\n time: " + t + "\n Hit: " + hit);
	}
}