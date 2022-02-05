using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [HideInInspector] public int health;
    public int maxHealth = 100;
    [HideInInspector] public bool bAlive = true;

    public virtual void TakeDamage(int damage)
    {
        if(bAlive)
        {
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
}
