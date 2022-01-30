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
                //Debug.Log("Explosion Hit " + rb.gameObject.name);
                if(rb.gameObject.tag.Equals("Player"))
                {
                    Vector3 prePos = rb.transform.position;
                    rb.AddExplosionForce(force, transform.position, radius);
                    Debug.Log("Player moved " + (rb.transform.position - prePos));
                }
                    
                    //StartCoroutine(KnockPlayer(rb,force,radius));
                else
                    rb.AddExplosionForce(force, transform.position, radius);
            }
        }
    }

    IEnumerator KnockPlayer(Rigidbody rb, float force, float radius)
    {
        rb.AddForce(Vector3.up * 4, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        //Vector3 direction = rb.transform.position - transform.position;
        //direction.y = 0;
        //rb.AddForce(direction.normalized * 6, ForceMode.Impulse);
        rb.AddExplosionForce(10000, transform.position, radius);
    }
}
