using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStatusEffect : MonoBehaviour
{
    public Transform target;

    protected EntityStats entity;

    void Update()
    {
        transform.position = target.position + Vector3.down;
    }

    public virtual void Initialize(Transform target, float duration = 3f, int modifier = 2)
    {
        if(target.gameObject.TryGetComponent<EntityStats>(out entity))
        {
            this.target = transform;
            StartCoroutine("Tick");
            Destroy(gameObject, duration);
        }
        else
            Destroy(gameObject);
    }

    protected virtual void Effect()
    {

    }

    protected virtual IEnumerator Tick()
    {
            Effect();
            yield return new WaitForSeconds(1);
            yield return Tick();
    }
}
