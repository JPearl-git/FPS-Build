using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : IWeapon
{
    [Header("Melee Details")]
    public ParticleSystem attackTrail;

    protected WeaponAnimation wAnim;

    public override void Equip(GunHUD gHUD, DetectionNotice detectionNotice, WeaponAnimation weaponAnimation)
    {
        base.Equip(gHUD, detectionNotice, weaponAnimation);
        gunHUD.SetCount(-1, -1);
        gunHUD.SetReserve(-1);

        wAnim = GetComponentInParent<WeaponAnimation>();
    }

    public override void Attack()
    {
        if(wAnim == null)
            return;

       //meleeMovement.ChangePosition(5f);
       wAnim.AnimateAttack();
       attackTrail.Play();
    }
}
