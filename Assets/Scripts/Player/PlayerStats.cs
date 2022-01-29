using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerStats : MonoBehaviour
{
    HUD_UI hud;
    InputManager inputManager;

    public int health, maxHealth = 100;
    [HideInInspector] public bool bAlive = true;

    void Awake()
    {
        hud = GameObject.Find("HUD").GetComponent<HUD_UI>();
        inputManager = gameObject.GetComponent<InputManager>();

        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(bAlive)
        {
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

    public void RestoreHealth(int heal)
    {
        if(bAlive)
        {
            health += heal;
            if(health > maxHealth)
                health = maxHealth;
                
            hud.SetHealth((float)health / maxHealth);
        }
    }

    private void Death()
    {
        inputManager.bCanControl = false;
        hud.PlayDeathFade();
    }
}
