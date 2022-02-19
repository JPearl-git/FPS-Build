using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponAnimation : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    GunSlot slot;

    int targetPosition, currentPosition = -1;
    float speed;
    bool bCanChange = true;

    void Awake()
    {
        TryGetComponent<GunSlot>(out slot);
        animator = GetComponent<Animator>();
    }

    public void EndReload()
    {
        animator.SetBool("isReloading", false);

        if(slot != null)
        {
            var gun = slot.Weapon as Gun;
            if(gun != null)
                gun.EndReload(true);
        }
    }

    public void Reset()
    {
        animator.SetBool("isSwinging", false);
    }

    public void AnimateAttack()
    {
        animator.SetBool("isSwinging", true);
    }

    void EndMeleeAttack()
    {
        var melee = slot.Weapon as MeleeWeapon;

        if(slot == null)
        {
            melee.attackTrail.Stop();
            return;
        }

        melee.ResetTargets();
        if(slot.bAttackAction)
            return;

        animator.SetBool("isSwinging", false);
        melee.bSwinging = false;

        if(melee != null)
            melee.attackTrail.Stop();
    }
}
