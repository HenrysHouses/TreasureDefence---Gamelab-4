/*
 * Written by:
 * Henrik
*/
using UnityEngine;

public class attackData
{
	public GameObject gameObject;
	public Transform transform;
	public EnemyBehaviour enemy;
	public Transform target;
	public Vector3 startPos;
	public float projectileSpeed;
	public int projectileDamage;
	public float t;
	public bool hit, curve;

	virtual public Vector3 UpdateProjectile()
    {
        if(target)
        {
            if (curve)
            {
                t = t + Time.deltaTime * projectileSpeed;
                t = Mathf.Clamp(t, 0, 1);
                if(t >= 1)
                {
                    hit = true;
                    return target.position;
                }
                return Vector3.Slerp(startPos, target.position, t);
            }
            
            t = t + Time.deltaTime * projectileSpeed;
            t = Mathf.Clamp(t, 0, 1);
            if(t >= 1)
            {
                hit = true;
                return target.position;
            }
            return Vector3.Lerp(startPos, target.position, t);
        }
        return transform.position;
    }

	public void print()
	{
		Debug.Log("GameObject: " + gameObject + "\n Transform: " + transform  + "\n enemy: " + enemy + "\n target: " + target + "\n startPos: " + startPos + "\n projectileSpeed: " + projectileSpeed + "\n projectileDamage: " + projectileDamage + "\n time: " + t + "\n Hit: " + hit);
	}
}