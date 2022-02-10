using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
    BURN,
    NONE
}

public enum HealthStatus
{
    NORMAL,
    FULL,
    LOW,
    VERY_LOW
}

public class EntityStats : MonoBehaviour
{
    [HideInInspector] public Dictionary<StatusEffect, IStatusEffect> activeStatus = new Dictionary<StatusEffect, IStatusEffect>();
    [HideInInspector] public bool bAlive = true;
    [HideInInspector] public int health;

    [Header("Basic Entity Variables")]
    public int maxHealth = 100;
    [Range(1f,5f)] public float CriticalMultplier = 1f;
    public bool bCanTakeStatusEffect;

    protected Vector3 lastHitDirection;

    public virtual void TakeDamage(int damage, bool bCritHit = false)
    {
        if(bAlive)
        {
            if(bCritHit)
                damage = Mathf.CeilToInt(damage * CriticalMultplier);

            health -= damage;
            if(health < 0)
                health = 0;

            if(health <= 0 && bAlive)
            {
                bAlive = false;
                Death();
            }
        }
    }

    public virtual void TakeDamage(int damage, Vector3 hitDirection, bool bCritHit = false)
    {
        lastHitDirection = hitDirection;
        TakeDamage(damage, bCritHit);
    }

    public virtual void RestoreHealth(int heal)
    {
        if(bAlive)
        {
            health += heal;
            if(health > maxHealth)
                health = maxHealth;
        }
    }

    protected virtual void Death(){}

    public void InflictStatus(StatusEffect status, float duration = 3, int modifier = 2)
    {
        if(status != StatusEffect.NONE)
        {
            if(activeStatus.TryGetValue(status, out IStatusEffect current))
            {
                modifier = Mathf.Max(modifier, current.modifier);
                duration += Time.timeSinceLevelLoad - current.startTime;
                activeStatus.Remove(status);
                
                current.type = StatusEffect.NONE;
                Destroy(current.gameObject);
            }

            GameObject statusObj = null;

            switch(status)
            {
                case StatusEffect.BURN:
                    statusObj = Instantiate(Resources.Load<GameObject>("Prefab/BurningEffect"), transform.position + Vector3.down, Quaternion.identity, transform);
                    break;
                default:
                    break;
            }

            if(statusObj != null)
            {
                if(statusObj.TryGetComponent<IStatusEffect>(out IStatusEffect effect))
                {
                    activeStatus.Add(status, effect);
                    effect.Initialize(this, duration, modifier);
                }
                else
                    Destroy(statusObj);
            }
        }
    }
}
