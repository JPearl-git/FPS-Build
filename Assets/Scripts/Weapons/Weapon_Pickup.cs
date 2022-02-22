using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pickup : IPickup
{
    public GameObject Weapon;
    bool hasWeapon;

    new void Start()
    {
        if(Weapon == null)
            return;

        Initialize(Weapon);
        bRequiresInput = true;
        base.Start();
    }

    public void Initialize(GameObject weaponObj)
    {
        if(hasWeapon)
            return;

        if(weaponObj.TryGetComponent<IWeapon>(out IWeapon iWeapon))
        {
            Quaternion rotation = transform.rotation;
            if(iWeapon.GetType() == typeof(MeleeWeapon))
                rotation = Quaternion.Euler(90,0,0);

            Weapon = Instantiate(weaponObj, transform.position, rotation, transform);

            float scale = iWeapon.scale;
            Weapon.transform.localScale = new Vector3(scale, scale, scale);

            hasWeapon = true;
        }
    }

    protected override void Pickup(Collider other)
    {
        if(!hasWeapon)
            return;

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
