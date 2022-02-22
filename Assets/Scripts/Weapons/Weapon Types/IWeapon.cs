using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWeapon : MonoBehaviour
{
    #region Hidden Components
    protected HitMarker hitMarker;
    protected GunHUD gunHUD;
    protected DetectionNotice detectionNotice;
    [HideInInspector] public IWeaponAnimation weaponAnimation;
    #endregion

    #region Weapon Details
    [Header("Weapon Details")]
    public string Name;
    public int damage;
    [HideInInspector] public bool bPressed;
    #endregion

    #region  Transform Variables
    [Header("Transform Variables")]
    public Vector3 offsetPos;
    public Vector3 offsetRot;
    public float scale = 1f;
    #endregion

    [Header("Attack Delay")]
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

    public abstract void AIEquip();

    public abstract void Attack();

    public virtual void Unequip()
    {
        if(weaponAnimation == null)
            return;

        weaponAnimation.animator.SetTrigger("Reset");
    }

    // This is for thrown weapons
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<Destructible>(out Destructible target))
        {
            if(TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                int dmg = Mathf.CeilToInt(rb.velocity.magnitude * 1.5f);
                target.GetHit(dmg, -rb.velocity, rb.position, false);
                Debug.Log("Hit Enemy for " + dmg + " health");
            }
        }
    }
}
