using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSlot : MonoBehaviour
{
    public Gun gun;
    public GunHUD gunHUD;

    void Start()
    {
        SetGun();
    }

    public void SetGun()
    {
        if(this.gun != null)
            this.gun.bPressed = false;

        if(transform.GetChild(0).TryGetComponent<Gun>(out Gun gun))
        {
            this.gun = gun;
            gun.Initialize(gunHUD);
        }
        else
        {
            gun = null;
            Debug.Log("No Gun Equipped");
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if(gun != null)
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
