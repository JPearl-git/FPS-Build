using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningEffect : IStatusEffect
{
    int damage;

    public override void Initialize(EntityStats entity, float duration = 3f, int modifier = 2)
    {
        damage = modifier;
        base.Initialize(entity, duration);
    }

    protected override void Effect()
    {
        entity.TakeDamage(damage);
    }
}
