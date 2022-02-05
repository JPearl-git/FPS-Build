using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : IPickup
{
    public int health = 15;

    protected override void Pickup(Collider other)
    {
        if(other.TryGetComponent<PlayerStats>(out PlayerStats ps))
        {
            if(ps.health < ps.maxHealth)
            {
                ps.RestoreHealth(health);
                Destroy(gameObject);
            }
        }
    }
}
