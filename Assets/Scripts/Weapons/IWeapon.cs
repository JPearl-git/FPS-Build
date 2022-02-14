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
    [Range(1,1000)]public int rpm = 100;
    public bool bAutomatic;

    [HideInInspector] public bool bPressed;

    [Header("Other Variables")]
    public Vector3 offsetPos, offsetRot;

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
