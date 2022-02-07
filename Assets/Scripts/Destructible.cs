using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : EntityStats
{
    [HideInInspector] public bool bCanHit = true;
    public bool bCritical;

    protected void Start()
    {
        Initialize();
    }

    public HitMarkerType GetHit(int damage, Vector3 hitDirection)
    {
        if(bCanHit)
        {
            TakeDamage(damage, hitDirection);

            if(health <= 0)
            {
                bCanHit = false;
                Death();
                return HitMarkerType.Kill;
            }
            
            if(bCritical)
                return HitMarkerType.Critical;

                return HitMarkerType.Normal;
        }
        else
            Debug.Log("Can't Hit " + gameObject.name);
        
        return HitMarkerType.None;
    }

    public virtual void Initialize()
    {
        health = maxHealth;
    }

    public virtual void Initialize(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }
}
