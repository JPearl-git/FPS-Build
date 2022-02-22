using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player_WeaponAnimation : IWeaponAnimation
{
    GunSlot slot;

    protected new void Awake()
    {   
        base.Awake();
        TryGetComponent<GunSlot>(out slot);
    }

    public override void EndReload()
    {
        base.EndReload();

        if(slot != null)
        {
            var gun = slot.Weapon as Gun;
            if(gun != null)
                gun.EndReload(true);
        }
    }

    protected override void EndMeleeAttack()
    {
        var melee = slot.Weapon as MeleeWeapon;

        if(slot == null)
        {
            melee.attackTrail.Stop();
            return;
        }

        if(melee != null)
            melee.ResetTargets();
            
        if(slot.bAttackAction)
            return;

        animator.SetBool("isSwinging", false);
        melee.bSwinging = false;

        if(melee != null)
            melee.attackTrail.Stop();
    }
}
