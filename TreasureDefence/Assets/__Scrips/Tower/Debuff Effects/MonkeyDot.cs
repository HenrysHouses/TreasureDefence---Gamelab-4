using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyDot : EnemyDebuff
{
    public int DotDamage;

    public float DotInterval;
    float DotTime;

    public Transform monkeyObject;

    public override void Start()
    {
        base.Start();
        DotTime = Time.time;
    }

    public override void debuffEffect(EnemyBehaviour effectedTarget)
    {
        if(Time.time - DotTime > DotInterval)
        {
            effectedTarget.TakeDamage(DotDamage);
            DotTime = Time.time;
        }    
        monkeyObject.position = effectedTarget.transform.position;
    }

    public override void ExpirationTrigger(EnemyBehaviour effectedTarget)
    {
        GameObject.Destroy(monkeyObject.gameObject);
    }
}
