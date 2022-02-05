using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
    BURN,
    NONE
}

public class EntityStats : MonoBehaviour
{
    [HideInInspector] public Dictionary<StatusEffect, IStatusEffect> activeStatus = new Dictionary<StatusEffect, IStatusEffect>();
    [HideInInspector] public int health;
    public int maxHealth = 100;
    [HideInInspector] public bool bAlive = true;

    public bool bCanTakeStatusEffect;

    public virtual void TakeDamage(int damage, bool bHasSource = false)
    {
        if(bAlive)
        {
            health -= damage;
            if(health < 0)
                health = 0;

            if(health <= 0 && bAlive && !bHasSource)
            {
                bAlive = false;
                Death();
            }
        }
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
