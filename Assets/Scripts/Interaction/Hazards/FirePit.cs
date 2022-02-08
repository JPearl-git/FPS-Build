using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePit : IFireTrap
{
    [SerializeField] Material M_On, M_Off;

    GameObject parent;

    ParticleSystem parentPS;

    void Start()
    {
        if(transform.parent != null)
        {
            parent = transform.parent.gameObject;
            parentPS = parent.GetComponent<ParticleSystem>();
        }

        ps = GetComponent<ParticleSystem>();
        base.Start();
    }

    public override void Activate()
    {
        base.Activate();
        if(parentPS)
            ToggleParent();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        if(parentPS)
            ToggleParent();
    }

    void ToggleParent()
    {
        if(bActive)
        {
            parentPS.Play();
            SetParentMaterial(M_On);
        }
        else
        {
            parentPS.Stop();
            SetParentMaterial(M_Off);
        }
    }

    void SetParentMaterial(Material material)
    {
        Material[] newMaterials = parent.GetComponent<MeshRenderer>().materials;
        newMaterials[1] = material;
        parent.GetComponent<MeshRenderer>().materials = newMaterials;
    }

    protected IEnumerator ApplyBurn(EntityStats entity)
    {
        Debug.Log("Apply Burn");
        if(entities.Contains(entity) && entity.gameObject != null)
        {
            entity.InflictStatus(StatusEffect.BURN, 8, 6);
            yield return new WaitForSeconds(8f);
            yield return ApplyBurn(entity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        AddCollider(other);
    }

    void OnTriggerExit(Collider other)
    {
        RemoveCollider(other);
    }
}
