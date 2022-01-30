using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : Destructible
{
    public override void Death()
    {
        StartCoroutine("Explode");
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
