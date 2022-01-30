using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : Destructible
{
    [SerializeField] GameObject explosion;
    public override void Death()
    {
        var exp = Instantiate(explosion, transform.position, transform.rotation);
        exp.transform.localScale = new Vector3(3,3,3);
        Destroy(gameObject,0.1f);
    }
}
