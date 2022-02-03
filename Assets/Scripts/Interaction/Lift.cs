using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public float force = 20f;
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            //rb.AddForce(transform.up * force, ForceMode.Acceleration);
            rb.velocity += (Vector3.up * force);
        }
    }
}
