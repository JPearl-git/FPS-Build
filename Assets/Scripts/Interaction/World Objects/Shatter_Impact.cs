using System.Collections;
using UnityEngine;

// Credit to yt/Peer Play
public class Shatter_Impact : MonoBehaviour
{
    public Transform brokenObject;
    public float impactMagnitude, overlapRadius, power, upwardPower;

    void OnTriggerEnter(Collider other)
    {
        /*
        Debug.Log("Hit by " + other.name);
        Rigidbody rb = other.attachedRigidbody;
        if(rb == null)
            return;

        Debug.Log(other.name + " velocity is " + rb.velocity.magnitude);
        if(rb.velocity.magnitude > impactMagnitude)
        {
            Destroy(gameObject);
            Instantiate(brokenObject, transform.position, transform.rotation);
            //brokenObject.localScale = transform.localScale

            Collider[] colliders = Physics.OverlapSphere(transform.position, overlapRadius);
            foreach (var hit in colliders)
            {
                if(hit.attachedRigidbody)
                    hit.attachedRigidbody.AddExplosionForce(power * rb.velocity.magnitude, transform.position, overlapRadius, upwardPower);
            }
        }
        */
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.relativeVelocity.magnitude > impactMagnitude)
        {
            Destroy(gameObject);

            Instantiate(brokenObject, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(transform.position, overlapRadius);
            foreach (var hit in colliders)
            {
                if(hit.attachedRigidbody)
                    hit.attachedRigidbody.AddExplosionForce(power * other.relativeVelocity.magnitude, transform.position, overlapRadius, upwardPower);
            }

            if(other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
                rb.AddForce(other.relativeVelocity * 5, ForceMode.Impulse);
        }
    }
}
