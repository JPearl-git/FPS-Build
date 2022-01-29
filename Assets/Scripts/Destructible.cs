using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int health, maxHealth;
    public bool bCritical, bCanHit = true;

    protected RaycastHit lastHit;

    void Start()
    {
        health = maxHealth;
        Initialize();
    }

    public HitMarkerType TakeDamage(int damage, RaycastHit hit)
    {
        if(bCanHit)
        {
            lastHit = hit;
            health -= damage;
            GetHit(damage);

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

    public virtual void Death()
    {
        // Original
    }

    public virtual void Initialize()
    {
        // Original
    }

    public virtual void GetHit(int damage)
    {
        // Original
    }
}
