using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWeapon : MonoBehaviour
{
    protected HitMarker hitMarker;
    protected GunHUD gunHUD;
    protected DetectionNotice detectionNotice;
    [HideInInspector] public IWeaponAnimation weaponAnimation;

    [Header("Weapon Details")]
    public string Name;
    public int damage;
    [Range(.1f,10f)]public float reloadSpeed = 1;
    
    public bool bAutomatic;

    [HideInInspector] public bool bPressed;

    [Header("Dimensions")]
    public Vector3 offsetPos;
    public Vector3 offsetRot;
    public float scale = 1f;

    protected float delayTime;

    [SerializeField] protected AudioSource sound;

    public virtual void Equip(GunHUD gHUD, DetectionNotice detectionNotice, GunSlot gunSlot)
    {
        bPressed = false;
        gunHUD = gHUD;
        gunHUD.SetName(Name);

        hitMarker = gHUD.GetComponent<HitMarker>();

        this.detectionNotice = detectionNotice;
        this.weaponAnimation = gunSlot.weaponAnimation;
        weaponAnimation.animator.ResetTrigger("Reset");
    }

    public virtual void AIEquip(){}

    public virtual void Attack(){}

    public virtual void Unequip()
    {
        if(weaponAnimation == null)
            return;

        weaponAnimation.animator.SetTrigger("Reset");
    }
}
