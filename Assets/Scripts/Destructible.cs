using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : EntityStats
{
    [HideInInspector] public bool bCanHit = true;

    protected void Start()
    {
        Initialize();
    }

    public virtual HitMarkerType GetHit(int damage, RaycastHit hit, bool bCritHit = false)
    {
        if(bCanHit)
        {
            TakeDamage(damage, hit.normal, bCritHit);

            if(health <= 0)
            {
                bCanHit = false;
                Death();
                return HitMarkerType.Kill;
            }
            
            if(bCritHit)
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
