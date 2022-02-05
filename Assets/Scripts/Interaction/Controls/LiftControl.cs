using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleCollisionEvent))]
public class LiftControl : ISwitchable
{
    [SerializeField] Material m_off, m_on;
    [SerializeField] Lift lift;
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        base.Start();
    }

    void SetMaterial(Material material)
    {
        Material[] newMaterials = GetComponent<MeshRenderer>().materials;
        newMaterials[1] = material;
        GetComponent<MeshRenderer>().materials = newMaterials;
    }

    public override void Activate()
    {
        ps.Play();
        lift.bActive = true;
        SetMaterial(m_on);
    }

    public override void Deactivate()
    {
        ps.Stop();
        lift.bActive = false;
        SetMaterial(m_off);
    }

    
}
