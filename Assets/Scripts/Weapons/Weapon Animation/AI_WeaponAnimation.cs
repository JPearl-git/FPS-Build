using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_WeaponAnimation : IWeaponAnimation
{
    [HideInInspector] public Gun gunScript;
    [HideInInspector] public BotStats bot;

    // Start is called before the first frame update
    public override void EndReload()
    {
        base.EndReload();

        if(gunScript != null)
            gunScript.EndReload(true);

        bot.bReloading = false;
    }

    protected override void EndMeleeAttack()
    {
        //Need to implement melee weapons for AI first
    }
}
