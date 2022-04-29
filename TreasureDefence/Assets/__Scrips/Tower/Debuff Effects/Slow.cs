using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : EnemyDebuff
{
    public Transform Transform;
    bool isApplied;
    public modifier effect;

    public override void debuffEffect(EnemyBehaviour effectedTarget)
    {
        if(!isApplied)
        {
            effectedTarget.speedMod.Add(effect);
            isApplied = true;
        }
        Transform.position = effectedTarget.transform.position;
    }

    public override void ExpirationTrigger(EnemyBehaviour effectedTarget)
    {
        effectedTarget.speedMod.Remove(effect);
        GameObject.Destroy(Transform.gameObject);
    }
}
