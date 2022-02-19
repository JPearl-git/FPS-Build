using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : IWeapon
{
    [Header("Melee Details")]
    public ParticleSystem attackTrail;
    [HideInInspector] public bool bSwinging;

    protected IWeaponAnimation wAnim;
    List<EntityStats> targetGroup = new List<EntityStats>();

    public override void Equip(GunHUD gHUD, DetectionNotice detectionNotice, GunSlot gunSlot)
    {
        base.Equip(gHUD, detectionNotice, gunSlot);
        gunHUD.SetCount(-1, -1);
        gunHUD.SetReserve(-1);

        wAnim = GetComponentInParent<IWeaponAnimation>();
    }

    public override void Attack()
    {
        if(wAnim == null)
            return;

       wAnim.AnimateAttack();
       attackTrail.Play();
       bSwinging = true;
    }

    public override void Unequip()
    {
        base.Unequip();
        weaponAnimation.animator.SetBool("isSwinging", false);
        attackTrail.Stop();
        bSwinging = false;
    }

    public void ResetTargets()
    {
        targetGroup = new List<EntityStats>();
    }

    protected void DealHit(EntityStats entity)
    {
        if(targetGroup.Contains(entity))
            return;

        Destructible dTarget = entity as Destructible;
        if(dTarget != null)
        {
            if(dTarget.bCanHit)
                hitMarker.HitTarget(dTarget.GetHit(damage, -transform.forward, Vector3.zero));
        }
        else if(entity.bAlive)
            entity.TakeDamage(damage, -transform.forward);

        if(entity != null)
            targetGroup.Add(entity);
    }

    void OnTriggerEnter(Collider other)
    {
        if(!bSwinging)
            return;

        // Damage Entity Types
        if(other.TryGetComponent<EntityStats>(out EntityStats entity))
            DealHit(entity);
    }
}
