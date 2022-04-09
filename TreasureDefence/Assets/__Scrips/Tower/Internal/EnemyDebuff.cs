using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
abstract public class EnemyDebuff
{
    float ApplyTime;
    public float Duration;
    bool HasExpired;

    virtual public void Start()
    {
        ApplyTime = Time.time;
        HasExpired = false;
    }
    private void DurationUpdate()
    {
        if(Time.time - ApplyTime > Duration)
        {
            HasExpired = true;
            ExpirationTrigger();
            Debug.Log("effect expired");
        }
    }

    public bool Update(EnemyBehaviour effectedTarget)
    {
        DurationUpdate();
        if(HasExpired)
        {
            return false;
        }
        debuffEffect(effectedTarget);
        return true;
    }

    public abstract void ExpirationTrigger();

    public abstract void debuffEffect(EnemyBehaviour effectedTarget);
}