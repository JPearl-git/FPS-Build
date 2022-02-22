using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Gun : Gun
{
    [Header("Explosion Details")]
    [SerializeField] GameObject explosionPrefab;
    public float radius, force, expSize;
    public int expDamage;

    protected override void HitTarget(RaycastHit hit)
    {
        base.HitTarget(hit);

        var exp = Instantiate(explosionPrefab, hit.point, Quaternion.identity);
        if(exp.TryGetComponent<Explosion>(out Explosion explosion))
        {
            explosion.transform.localScale = new Vector3(expSize, expSize, expSize);
            explosion.Init(radius, force, expDamage);
        }
        else
            Destroy(exp);
    }
}
