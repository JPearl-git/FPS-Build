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
        {
            Quaternion rotation = transform.rotation;
            if(iWeapon.GetType() == typeof(MeleeWeapon))
                rotation = Quaternion.Euler(90,0,0);

            Weapon = Instantiate(weaponObj, transform.position, rotation, transform);

            float scale = iWeapon.scale;
            Weapon.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    protected override void Pickup(Collider other)
    {
        GunSlot slot = other.GetComponentInChildren<GunSlot>();
        if(slot != null && this.Weapon.GetComponent<IWeapon>())
        {
            slot.bCanAttack = false;
            slot.Equip(Instantiate(this.Weapon));
            slot.bCanAttack = true;

            Destroy(gameObject);
        }
    }
}
