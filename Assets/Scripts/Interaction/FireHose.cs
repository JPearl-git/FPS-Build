using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireHose : ISwitchable
{
    ParticleSystem ps;

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

    void OnTriggerEnter(Collider other)
    {
        if(bActive && other.tag.Equals("Player"))
        {
            Debug.Log("You're on Fire!");
        }
    }
}
