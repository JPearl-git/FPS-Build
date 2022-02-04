using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireHose : MonoBehaviour
{
    public bool bActive;

    ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();

        Activate();
    }

    public void Activate()
    {
        if(!bActive)
        {
            bActive = true;
            ps.Play();
        }
    }

    public void Deactivate()
    {
        if(bActive)
        {
            bActive = false;
            ps.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(bActive && other.tag.Equals("Player"))
        {
            Debug.Log("You're on Fire!");
        }
    }
}
