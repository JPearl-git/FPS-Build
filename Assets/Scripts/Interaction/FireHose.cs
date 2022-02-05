using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireHose : ISwitchable
{
    ParticleSystem ps;

    List<EntityStats> entities = new List<EntityStats>();

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        base.Start();
    }

    public override void Activate()
    {
        bActive = true;
        ps.Play();
    }

    public override void Deactivate()
    {
        bActive = false;
        ps.Stop();
    }

    IEnumerator ApplyBurn(EntityStats entity)
    {
        if(entities.Contains(entity))
        {
            entity.InflictStatus(StatusEffect.BURN);
            yield return new WaitForSeconds(3f);
            yield return ApplyBurn(entity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(bActive && other.TryGetComponent<EntityStats>(out EntityStats entity))
        {
            if(entity.bCanTakeStatusEffect)
            {
                entities.Add(entity);
                StartCoroutine(ApplyBurn(entity));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(bActive && other.TryGetComponent<EntityStats>(out EntityStats entity))
        {
            entities.Remove(entity);
        }
    }
}
