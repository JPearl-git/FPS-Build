using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : Destructible
{
    [SerializeField] GameObject explosion;
    public override void Death()
    {
        StartCoroutine("Explode");
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.1f);
        var exp = Instantiate(explosion, transform.position, transform.rotation);
        exp.transform.localScale = new Vector3(3,3,3);
        Destroy(gameObject);
    }
}
