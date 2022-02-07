using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : EntityStats
{
    [HideInInspector] public bool bCanHit = true;
    public bool bCritical;
    
    protected Vector3 lastHit;

    protected void Start()
    {
        health = maxHealth;
        Initialize();
    }

    public HitMarkerType GetHit(int damage, Vector3 hitDirection)
    {
        if(bCanHit)
        {
            lastHit = hitDirection;
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
        // Original
    }
}
