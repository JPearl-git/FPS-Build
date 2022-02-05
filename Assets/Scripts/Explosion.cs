using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //KnockBack();
        //Destroy(gameObject,0.25f);
    }

    public void Init(float radius, float force)
    {
        GetComponent<ParticleSystem>().Play();

        KnockBack(radius, force);
        if(TryGetComponent<AudioSource>(out AudioSource audio))
            audio.Play();

        Destroy(gameObject,1f);
    }

    void KnockBack(float radius, float force)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if(rb != null)
            {
                if(rb.TryGetComponent<PhysicsMovement>(out PhysicsMovement pm))
                {
                    if(pm.CheckGrounded())
                    {
                        rb.transform.position += (Vector3.up * 0.5f);
                        pm.ForcedLaunch();
                    }
                }
                    rb.AddExplosionForce(force, transform.position, radius);
            }
        }
    }
}
