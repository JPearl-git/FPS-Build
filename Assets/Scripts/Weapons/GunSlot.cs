using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player_WeaponAnimation)), RequireComponent(typeof(Object_Launcher))]
public class GunSlot : MonoBehaviour
{
    #region Hidden Components
    [HideInInspector] public IWeapon Weapon;
    [HideInInspector] public Player_WeaponAnimation weaponAnimation;
    DetectionNotice detectionNotice;
    Object_Launcher launcher;

    /*[HideInInspector]*/ public GameObject[] weaponObjects = new GameObject[MaxSlots];

    [HideInInspector] public bool bCanAttack, bAttackAction;
    #endregion

    public GunHUD gunHUD;
    public RecoilControl recoilControl;
    public const int MaxSlots = 4;
    int currentEquippedGun;

    void Start()
    {
        if(transform.childCount > 0)
        {
            weaponObjects[0] = Weapon.gameObject;
            SetWeapon(0);
        }

        TryGetComponent<Player_WeaponAnimation>(out weaponAnimation);
        TryGetComponent<Object_Launcher>(out launcher);

        var control = GameObject.Find("Level Control");
        control.TryGetComponent<DetectionNotice>(out detectionNotice);
    }

    #region Weapon Functions

    ///<summary> Setup the Controls and Visuals for the Weapon in a specific slot of the Weapon Object Array</summary>
    ///<param name="slot">Slot in the Weapon Object array</param>
    public void SetWeapon(int slot)
    {
        // Check if there is currently a Weapon Script in use
        if(this.Weapon != null)
            this.Weapon.bPressed = false;

        // Set up the Gun Data from the current slot
        if(transform.GetChild(slot).TryGetComponent<IWeapon>(out IWeapon weapon))
        {
            this.Weapon = weapon;
            weapon.Equip(gunHUD, detectionNotice, this);
            bCanAttack = true;

            weaponObjects[slot] = weapon.gameObject;
            currentEquippedGun = slot;
        }
        // If there is no weapon, don't do anything
        else
        {
            weapon = null;
            bCanAttack = false;
            Debug.Log("No Gun Equipped");
        }

        // Deactivate all other weapons
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

    ///<summary>Equip weapon as a child of this GameObject</summary>
    ///<param name="newWeapon">GameObject containing a IWeapon script</param>
    public void Equip(GameObject newWeapon)
    {
        // Unequip current weapon
        if(Weapon != null)
            Weapon.Unequip();
        
        // New gun takes up a free slot
        if(transform.childCount < MaxSlots)
        {
            newWeapon.transform.SetParent(transform);
            int currentSlot = transform.childCount - 1;
            SetWeapon(currentSlot);
        }
        // New gun replaces the current gun
        else
        {
            GameObject oldWeapon = Weapon.gameObject;
            newWeapon.transform.SetParent(transform);
            newWeapon.transform.SetSiblingIndex(currentEquippedGun);
            SetWeapon(currentEquippedGun);

            Destroy(oldWeapon);
        }

        IWeapon iWeapon = newWeapon.GetComponent<IWeapon>();
        newWeapon.transform.localPosition = iWeapon.offsetPos;
        newWeapon.transform.localRotation = Quaternion.Euler(iWeapon.offsetRot);
        newWeapon.transform.localScale = new Vector3(iWeapon.scale, iWeapon.scale, iWeapon.scale);

        bCanAttack = true;
    }

    void ReorderSlots(int slot, int removedWeapon)
    {
        Switch(slot);
        RemoveWeapon(removedWeapon);

        var temp = weaponObjects.Where(w => w != null).ToList();
        var newArray = new GameObject[MaxSlots];

        for (int i = 0; i < temp.Count; i++)
            newArray[i] = temp[i];

        weaponObjects = newArray;
        currentEquippedGun = Array.IndexOf(weaponObjects, Weapon.gameObject);
    }

    ///<summary>Switch to another weapon</summary>
    ///<param name="slot">Slot in the Weapon Object array</param>
    public void Switch(int slot)
    {
        if(slot != currentEquippedGun && weaponObjects[slot] != null)
        {
            bCanAttack = false;
            Weapon.Unequip();

            weaponObjects[slot].SetActive(true);
            SetWeapon(slot);
            bCanAttack = true;
        }
    }

    ///<summary>Remove current weapon</summary>
    public void DiscardCurrentWeapon()
    {
        if(AvailableWeapons() < 1)
            return;

        int dropNum = currentEquippedGun;
        if(dropNum == 0)
        {
            int availableWeapons = AvailableWeapons();
            if(availableWeapons > 1)
                ReorderSlots(availableWeapons - 1, dropNum);
            else
            {
                Destroy(transform.GetChild(0).gameObject);
                gunHUD.SetNull();
                RemoveWeapon(dropNum);
            }
        }
        else
            ReorderSlots(dropNum - 1, dropNum);
    }

    void RemoveWeapon(int slot)
    {
        if(launcher != null)
            launcher.LaunchObject(transform.localScale, weaponObjects[slot], true);

        weaponObjects[slot] = null;
        Destroy(transform.GetChild(slot).gameObject);
    }

    ///<summary>Return number of weapons in Weapon Object array</summary>
    public int AvailableWeapons()
    {
        return weaponObjects.Count(x => x != null);
    }
    #endregion

    #region Input Functions
    public void Attack(InputAction.CallbackContext context)
    {
        if(Weapon == null || !bCanAttack)
            return;

        if(context.performed)
        {
            bAttackAction = true;
            Weapon.bPressed = true;
            Weapon.Attack();
        }
        else if(context.canceled)
        {
            Weapon.bPressed = false;
            bAttackAction = false;
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

    public void Equip_Weapon_1(InputAction.CallbackContext context)
    {
        if(context.performed)
            Switch(0);
    }
    public void Equip_Weapon_2(InputAction.CallbackContext context)
    {
        if(context.performed)
            Switch(1);
    }
    public void Equip_Weapon_3(InputAction.CallbackContext context)
    {
        if(context.performed)
            Switch(2);
    }
    public void Equip_Weapon_4(InputAction.CallbackContext context)
    {
        if(context.performed)
            Switch(3);
    }

    public void Quick_Swap(InputAction.CallbackContext context)
    {
        if(!context.performed)
            return;

        int next = currentEquippedGun + 1;
        if(next == AvailableWeapons())
            next = 0;

        Switch(next);
    }

    public void Drop_Weapon(InputAction.CallbackContext context)
    {
        if(!context.performed)
            return;

        DiscardCurrentWeapon();
    }
    #endregion
}
