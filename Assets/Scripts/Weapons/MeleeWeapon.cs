using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : IWeapon
{
    [Header("Melee Details")]
    public ParticleSystem attackTrail;

    public override void Initialize(GunHUD gHUD, DetectionNotice detectionNotice)
    {
        base.Initialize(gHUD, detectionNotice);
        gunHUD.SetCount(-1, -1);
        gunHUD.SetReserve(-1);
    }

    public override void Attack()
    {
        Debug.Log("Attacking!");
    }
}
