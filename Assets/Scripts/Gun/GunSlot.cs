using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSlot : MonoBehaviour
{
    public Gun gun;
    public GunHUD gunHUD;

    [HideInInspector]
    public bool bCanShoot;

    void Start()
    {
        SetGun();
    }

    public void SetGun()
    {
        if(this.gun != null)
            this.gun.bPressed = false;
        else
            Debug.Log("No gun");

        if(transform.GetChild(0).TryGetComponent<Gun>(out Gun gun))
        {
            Debug.Log("Init " + gun.Name);
            this.gun = gun;
            gun.Initialize(gunHUD);
            bCanShoot = true;
        }
        else
        {
            gun = null;
            bCanShoot = false;
            Debug.Log("No Gun Equipped");
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
}
