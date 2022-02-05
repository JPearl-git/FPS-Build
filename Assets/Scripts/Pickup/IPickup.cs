using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IPickup : MonoBehaviour
{
    public float rotationSpeed = 25f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            Pickup(other);
        }
    }

    protected virtual void Pickup(Collider other){}
}
