using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GroundAnchor : MonoBehaviour
{
    Rigidbody rb;
    public float forceMultiplier = 100f;
    void OnTriggerEnter(Collider other)
    {
        if(!other.tag.Equals("Player"))
            return;

        rb = other.GetComponent<Rigidbody>();
        InvokeRepeating("Anchor", 0, 0.01f);
    }

    void OnTriggerExit(Collider other)
    {
        if(!other.tag.Equals("Player"))
            return;

        CancelInvoke("Anchor");
    }

    void Anchor()
    {
        if(rb == null)
            return;

        rb.AddForce(Vector3.down * forceMultiplier, ForceMode.Force);
    }
}
