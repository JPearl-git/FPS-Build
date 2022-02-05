using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pickup : IPickup
{
    public GameObject gun;

    void Start()
    {
        if(gun != null)
            Initialize(gun);
    }

    public void Initialize(GameObject gunObj)
    {
        if(gunObj.GetComponent<Gun>() != null)
            gun = Instantiate(gunObj, transform.position, transform.rotation, transform);
    }

    protected override void Pickup(Collider other)
    {
        GunSlot slot = other.GetComponentInChildren<GunSlot>();
        if(slot != null && this.gun.GetComponent<Gun>())
        {
            slot.bCanShoot = false;
            slot.Equip(Instantiate(this.gun));
            slot.bCanShoot = true;

            Destroy(gameObject);
        }
    }
}
