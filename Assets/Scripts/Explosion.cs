using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Explosion : MonoBehaviour
{
    float duration, distance;
    int damage, modifier;
    StatusEffect effect = StatusEffect.NONE;

    public void Init(float radius, float force, int damage)
    {
        this.damage = damage;
        GetComponent<ParticleSystem>().Play();

        KnockBack(radius, force);
        if(TryGetComponent<AudioSource>(out AudioSource audio))
            audio.Play();

        Destroy(gameObject,1f);
    }

    public void Init(float radius, float force, int damage, StatusEffect effect, float duration = 3f, int modifier = 2)
    {
        this.effect = effect;
        this.duration = duration;
        this.modifier = modifier;
        Init(radius, force, damage);
    }

    void KnockBack(float radius, float force)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if(rb != null)
            {
                distance = (transform.position - rb.position).magnitude;
                
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
            if(nearby.TryGetComponent<EntityStats>(out EntityStats entity))
            {
                entity.TakeDamage(Mathf.CeilToInt(damage / distance));

                if(effect != StatusEffect.NONE && entity.bCanTakeStatusEffect)
                {
                    entity.InflictStatus(effect, duration, modifier);
                }
            }
            
        }
    }
}
