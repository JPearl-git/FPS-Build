using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBarrel_Gun : Gun
{
    [SerializeField] Transform muzzleGroup;

    protected override void HitScan(ParticleSystem baseMuzzle)
    {
        foreach(Transform child in muzzleGroup)
        {
            if(child.gameObject.TryGetComponent<ParticleSystem>(out ParticleSystem ps))
                base.HitScan(ps);
        }
    }
}
