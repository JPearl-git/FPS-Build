using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : Destructible
{
    [SerializeField] GameObject explosion;

    public int damage = 8;

    public float size = 3f, radius = 3f, force = 3f;
    protected override void Death()
    {
        var exp = Instantiate(explosion, transform.position, transform.rotation);
        exp.transform.localScale = new Vector3(size,size,size);
        exp.GetComponent<Explosion>().Init(radius, force, damage, StatusEffect.BURN, 6f);
        Destroy(gameObject,0.1f);
    }
}
