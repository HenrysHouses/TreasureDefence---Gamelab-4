using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
abstract public class EnemyDebuff
{
    float ApplyTime;
    public float Duration;
    bool HasExpired;
    public GameObject gameObject;

    virtual public void Start()
    {
        ApplyTime = Time.time;
        HasExpired = false;
    }
    private void DurationUpdate(EnemyBehaviour effectedTarget)
    {
        if(Time.time - ApplyTime > Duration)
        {
            HasExpired = true;
            ExpirationTrigger(effectedTarget);
            Debug.Log("effect expired");
        }
    }

    public bool Update(EnemyBehaviour effectedTarget)
    {
        DurationUpdate(effectedTarget);
        if(HasExpired)
        {
            return false;
        }
        debuffEffect(effectedTarget);
        return true;
    }

    public abstract void ExpirationTrigger(EnemyBehaviour effectedTarget);

    public abstract void debuffEffect(EnemyBehaviour effectedTarget);
}