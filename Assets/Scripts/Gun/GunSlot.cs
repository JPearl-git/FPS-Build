using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSlot : MonoBehaviour
{
    public const int MaxSlots = 4;
    public Gun gun;
    public GunHUD gunHUD;
    DetectionNotice detectionNotice;

    public GameObject[] gunObjects = new GameObject[MaxSlots];

    [HideInInspector]
    public bool bCanShoot;
    int currentEquippedGun;

    void Start()
    {
        if(transform.childCount > 0)
        {
            gunObjects[0] = gun.gameObject;
            SetGun(0);
        }

        var control = GameObject.Find("Level Control");
        if(control.TryGetComponent<DetectionNotice>(out DetectionNotice notice))
            detectionNotice = notice;
    }

    public void SetGun(int slot)
    {
        // Check if there is currently a Gun Script in use
        if(this.gun != null)
            this.gun.bPressed = false;
        else
            Debug.Log("No gun");

        // Set up the Gun Data from the current slot
        if(transform.GetChild(slot).TryGetComponent<Gun>(out Gun gun))
        {
            this.gun = gun;
            gun.Initialize(gunHUD, detectionNotice);
            bCanShoot = true;

            gunObjects[slot] = gun.gameObject;
            currentEquippedGun = slot;
        }
        // If there is no gun, don't do anything
        else
        {
            gun = null;
            bCanShoot = false;
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
            SetGun(currentSlot);
        }
        // New gun replaces the current gun
        else
        {
            GameObject oldGun = gun.gameObject;
            newGun.transform.SetParent(transform);
            newGun.transform.SetSiblingIndex(currentEquippedGun);
            SetGun(currentEquippedGun);

            Destroy(oldGun);
        }

        newGun.transform.localPosition = newGun.GetComponent<Gun>().offset;
        newGun.transform.localRotation = Quaternion.identity;
        newGun.transform.localScale = Vector3.one;

        bCanShoot = true;
    }

    //Swap to a different weapon
    public void Switch(int slot)
    {
        if(slot != currentEquippedGun && gunObjects[slot] != null)
        {
            bCanShoot = false;
            gun.CancelActions();

            gunObjects[slot].SetActive(true);
            SetGun(slot);
            bCanShoot = true;
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if(gun != null && bCanShoot)
        {
            if(context.performed)
            {
                gun.bPressed = true;
                gun.Shoot();
            }
            else if(context.canceled)
                gun.bPressed = false;
        }
        // else
        //     Debug.Log("Cant Shoot");
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if(gun != null && context.performed)
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
