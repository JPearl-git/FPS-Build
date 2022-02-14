using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWeapon : MonoBehaviour
{
    protected HitMarker hitMarker;
    protected GunHUD gunHUD;
    protected DetectionNotice detectionNotice;

    [Header("Weapon Details")]
    public string Name;
    public int damage;
    [Range(.1f,1f)]public float reloadTime = .1f;
    
    public bool bAutomatic;

    [HideInInspector] public bool bPressed;

    [Header("Dimensions")]
    public Vector3 offsetPos;
    public Vector3 offsetRot;
    public float scale = 1f;

    protected float delayTime;

    [SerializeField] protected AudioSource sound;

    public virtual void Initialize(GunHUD gHUD, DetectionNotice detectionNotice)
    {
        bPressed = false;
        gunHUD = gHUD;
        gunHUD.SetName(Name);

        hitMarker = gHUD.GetComponent<HitMarker>();

        this.detectionNotice = detectionNotice;
    }

    public virtual void Attack(){}
}
