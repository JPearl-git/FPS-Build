using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class IFireTrap : ISwitchable
{
    protected ParticleSystem ps;

    protected List<EntityStats> entities = new List<EntityStats>();

    new void Start()
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

    protected virtual IEnumerator ApplyBurn(EntityStats entity)
    {
        if(entity == null)
            yield return null;

        if(entities.Contains(entity))
        {
            entity.InflictStatus(StatusEffect.BURN);
            yield return new WaitForSeconds(3f);
            yield return ApplyBurn(entity);
        }
    }

    protected void AddCollider(Collider other)
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

    protected void RemoveCollider(Collider other)
    {
        if(bActive && other.TryGetComponent<EntityStats>(out EntityStats entity))
        {
            entities.Remove(entity);
        }
    }
}
