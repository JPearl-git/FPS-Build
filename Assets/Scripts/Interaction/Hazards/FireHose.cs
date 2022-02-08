using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireHose : IFireTrap
{
    void OnTriggerEnter(Collider other)
    {
        AddCollider(other);
    }

    void OnTriggerExit(Collider other)
    {
        RemoveCollider(other);
    }
}
