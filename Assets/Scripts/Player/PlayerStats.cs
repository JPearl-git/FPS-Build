using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerStats : EntityStats
{
    HUD_UI hud;
    InputManager inputManager;

    void Awake()
    {
        hud = GameObject.Find("HUD").GetComponent<HUD_UI>();
        inputManager = gameObject.GetComponent<InputManager>();

        health = maxHealth;
    }

    public override void TakeDamage(int damage, bool bCritHit = false)
    {
        if(bAlive)
        {
            if(bCritHit)
                damage = Mathf.CeilToInt(damage * CriticalMultplier);

            health -= damage;
            if(health < 0)
                health = 0;

            hud.SetHealth((float)health / maxHealth);

            if(health <= 0 && bAlive)
            {
                bAlive = false;
                Death();
            }
        }
    }

    public override void RestoreHealth(int heal)
    {
        base.RestoreHealth(heal);
        hud.SetHealth((float)health / maxHealth);
    }

    protected override void Death()
    {
        inputManager.bCanControl = false;
        hud.PlayDeathFade();
    }
}
