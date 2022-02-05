using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStatusEffect : MonoBehaviour
{
    protected EntityStats entity;

    public virtual void Initialize(EntityStats entity, float duration = 3f, int modifier = 2)
    {
        if(entity != null)
        {
            this.entity = entity;
            StartCoroutine("Tick");
            Destroy(gameObject, duration);
        }
        else
            Destroy(gameObject);
    }

    protected virtual void Effect(){}

    protected virtual IEnumerator Tick()
    {
            Effect();
            yield return new WaitForSeconds(1);
            yield return Tick();
    }
}
