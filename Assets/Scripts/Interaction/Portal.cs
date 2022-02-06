using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Portal : ISwitchable
{
    [SerializeField] Portal otherSide;
    ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        base.Start();
    }

    public override void Activate()
    {
        ps.Play();
    }

    public override void Deactivate()
    {
        ps.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Debug.Log("Teleported");
            rb.position += transform.forward * 5f;
        }
    }
}
