using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pickup : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public GameObject gun;

    void Start()
    {
        if(gun != null)
            Initialize(gun);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void Initialize(GameObject gunObj)
    {
        if(gunObj.GetComponent<Gun>() != null)
        {
            gun = Instantiate(gunObj, transform.position, transform.rotation, transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            GunSlot slot = other.GetComponentInChildren<GunSlot>();
            if(slot != null && this.gun.GetComponent<Gun>())
            {
                slot.bCanShoot = false;
                //var oldGun = slot.transform.GetChild(0);
                slot.Equip(Instantiate(this.gun));
                //var newGun = Instantiate(this.gun, slot.transform);
                //newGun.transform.SetAsFirstSibling();
                //
                //slot.SetGun();
                //newGun.transform.localPosition = slot.gun.offset;
                //newGun.transform.localRotation = Quaternion.identity;
                //newGun.transform.localScale = Vector3.one;
                //
                //Destroy(oldGun.gameObject);
                slot.bCanShoot = true;

                Destroy(gameObject);
            }
        }
    }
}
