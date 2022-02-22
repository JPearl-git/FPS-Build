using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [HideInInspector]
    public bool bActive;
    public float force = 20f;
    void OnTriggerEnter(Collider other)
    {
        if(bActive && other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            if(other.TryGetComponent<PhysicsMovement>(out PhysicsMovement pm))
                pm.ForcedLaunch();
                
            //rb.AddForce(transform.up * force, ForceMode.Acceleration);
            rb.velocity += (Vector3.up * force);
        }
    }
}
