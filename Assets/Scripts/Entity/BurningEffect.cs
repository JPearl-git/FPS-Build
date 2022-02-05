using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningEffect : IStatusEffect
{
    int damage;

    public override void Initialize(Transform target, float duration = 3f, int modifier = 2)
    {
        damage = modifier;
        base.Initialize(target, duration);
    }
    
    void Update()
    {
        transform.position = target.position + Vector3.down;
    }

    protected override void Effect()
    {
        entity.TakeDamage(damage);
    }
}
