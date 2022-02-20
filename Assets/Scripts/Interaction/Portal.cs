using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Portal : ISwitchable
{
    [SerializeField] Portal otherSide;
    ParticleSystem ps;

    [HideInInspector] public List<Rigidbody> active;

    new void Start()
    {
        ps = GetComponent<ParticleSystem>();
        base.Start();
    }

    public override void Activate()
    {
        if(otherSide != null)
            otherSide.bActive = true;
        ps.Play();
    }

    public override void Deactivate()
    {
        if(otherSide != null)
            otherSide.bActive = false;
        ps.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if(bActive && otherSide != null)
        {
            if(other.TryGetComponent<Rigidbody>(out Rigidbody rb) && otherSide.bActive)
            {
                if(!active.Contains(rb) && !otherSide.active.Contains(rb))
                {
                    Debug.Log("Teleported");
                    otherSide.active.Add(rb);

                    float yOffset = other.transform.position.y - transform.position.y;
                    Vector3 position = otherSide.transform.position;
                    position.y += yOffset;

                    other.transform.position = position;
                    other.transform.rotation = otherSide.transform.rotation;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            active.Remove(rb);
        }
    }
}
