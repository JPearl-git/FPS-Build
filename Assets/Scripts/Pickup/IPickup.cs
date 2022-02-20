using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPickup : IButtonInteract
{
    public float rotationSpeed = 25f;
    public bool bRequiresInput;
    Collider inputCollider;

    protected virtual void Start()
    {
        bCanInteract = true;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(bRequiresInput)
        {
            inputCollider = other;
            base.OnTriggerEnter(other);
        }
        else if(other.tag.Equals("Player"))
            Pickup(other);
    }

    public override void Interact()
    {
        base.Interact();
        Pickup(inputCollider);
    }

    protected virtual void Pickup(Collider other){}
}
