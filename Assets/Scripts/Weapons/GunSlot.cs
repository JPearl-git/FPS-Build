using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSlot : MonoBehaviour
{
    public const int MaxSlots = 4;
    public IWeapon Weapon;
    public GunHUD gunHUD;
    DetectionNotice detectionNotice;

    public GameObject[] gunObjects = new GameObject[MaxSlots];

    [HideInInspector]
    public bool bCanAttack;
    int currentEquippedGun;

    void Start()
    {
        if(transform.childCount > 0)
        {
            gunObjects[0] = Weapon.gameObject;
            SetWeapon(0);
        }

        var control = GameObject.Find("Level Control");
        if(control.TryGetComponent<DetectionNotice>(out DetectionNotice notice))
            detectionNotice = notice;
    }

    public void SetWeapon(int slot)
    {
        // Check if there is currently a Gun Script in use
        if(this.Weapon != null)
            this.Weapon.bPressed = false;
        else
            Debug.Log("No gun");

        // Set up the Gun Data from the current slot
        if(transform.GetChild(slot).TryGetComponent<Gun>(out Gun gun))
        {
            this.Weapon = gun;
            gun.Initialize(gunHUD, detectionNotice);
            bCanAttack = true;

            gunObjects[slot] = gun.gameObject;
            currentEquippedGun = slot;
        }
        // If there is no gun, don't do anything
        else
        {
            gun = null;
            bCanAttack = false;
            Debug.Log("No Gun Equipped");
        }

        // Deactivate all other guns
        if(transform.childCount > 1)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                if(i == slot)
                    continue;
                else
                    transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    // Equip a new gun
    public void Equip(GameObject newGun)
    {
        // New gun takes up a free slot
        if(transform.childCount < MaxSlots)
        {
            newGun.transform.SetParent(transform);
            int currentSlot = transform.childCount - 1;
            SetWeapon(currentSlot);
        }
        // New gun replaces the current gun
        else
        {
            GameObject oldGun = Weapon.gameObject;
            newGun.transform.SetParent(transform);
            newGun.transform.SetSiblingIndex(currentEquippedGun);
            SetWeapon(currentEquippedGun);

            Destroy(oldGun);
        }

        newGun.transform.localPosition = newGun.GetComponent<Gun>().offsetPos;
        newGun.transform.localRotation = Quaternion.identity;
        newGun.transform.localScale = Vector3.one;

        bCanAttack = true;
    }

    //Swap to a different weapon
    public void Switch(int slot)
    {
        if(slot != currentEquippedGun && gunObjects[slot] != null)
        {
            bCanAttack = false;
            if(Weapon.gameObject.TryGetComponent<Gun>(out Gun gun))
                gun.CancelActions();

            gunObjects[slot].SetActive(true);
            SetWeapon(slot);
            bCanAttack = true;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if(Weapon != null && bCanAttack)
        {
            if(context.performed)
            {
                Weapon.bPressed = true;
                Weapon.Attack();
            }
            else if(context.canceled)
                Weapon.bPressed = false;
        }
        // else
        //     Debug.Log("Cant Shoot");
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if(Weapon != null && context.performed && Weapon.gameObject.TryGetComponent<Gun>(out Gun gun))
            gun.Reload();
        // else
        //     Debug.Log("Cant Reload");
    }

    public void EquipGun1(InputAction.CallbackContext context)
    {
        if(context.performed)
            Switch(0);
    }
    public void EquipGun2(InputAction.CallbackContext context)
    {
        if(context.performed)
            Switch(1);
    }
    public void EquipGun3(InputAction.CallbackContext context)
    {
        if(context.performed)
            Switch(2);
    }
    public void EquipGun4(InputAction.CallbackContext context)
    {
        if(context.performed)
            Switch(3);
    }
}
