using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : IWeapon
{
    [Header("Melee Details")]
    public ParticleSystem attackTrail;

    protected MeleeMovement meleeMovement;

    public override void Initialize(GunHUD gHUD, DetectionNotice detectionNotice)
    {
        base.Initialize(gHUD, detectionNotice);
        gunHUD.SetCount(-1, -1);
        gunHUD.SetReserve(-1);

        meleeMovement = GetComponentInParent<MeleeMovement>();
    }

    public override void Attack()
    {
        if(meleeMovement == null)
            return;

       //meleeMovement.ChangePosition(5f);
       meleeMovement.AnimateAttack();
       attackTrail.Play();
    }
}
