using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pickup : IPickup
{
    public GameObject Weapon;

    void Start()
    {
        if(Weapon != null)
            Initialize(Weapon);
    }

    public void Initialize(GameObject weaponObj)
    {
        if(weaponObj.TryGetComponent<IWeapon>(out IWeapon iWeapon))
            Weapon = Instantiate(weaponObj, transform.position, Quaternion.Euler(iWeapon.offsetRot), transform);
    }

    protected override void Pickup(Collider other)
    {
        GunSlot slot = other.GetComponentInChildren<GunSlot>();
        if(slot != null && this.Weapon.GetComponent<Gun>())
        {
            slot.bCanAttack = false;
            slot.Equip(Instantiate(this.Weapon));
            slot.bCanAttack = true;

            Destroy(gameObject);
        }
    }
}
