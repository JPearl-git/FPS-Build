using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    HitMarker hitMarker;
    GunHUD gunHUD;

    public string Name;
    public GameObject muzzle;
    public float range = float.MaxValue;
    [Range(0f,1f)] public float recoil;
    [Range(.1f,1f)]public float reloadTime = .1f;
    [Range(1,1000)]public int rpm = 100;
    public int damage;

    public int currentAmmo, clipSize, ammoReserve;
    public bool bAutomatic, bPressed;

    [SerializeField] ParticleSystem gunMuzzle, gunShot;
    [SerializeField] AudioSource sound;

    float delayTime, currentRecoil = 0;
    bool bReloading;

    void Start()
    {
        hitMarker = GameObject.Find("HUD").GetComponent<HitMarker>();
    }

    public void Initialize(GunHUD gHUD)
    {
        bPressed = false;
        gunHUD = gHUD;
        gunHUD.SetName(Name);
        gunHUD.SetCount(currentAmmo, clipSize);
        gunHUD.SetReserve(ammoReserve);
    }

    public bool CanShoot()
    {
        if(delayTime > 0) return false;
        if(bReloading) return false;
        if(currentAmmo < 1) return false;

        return bPressed;
    }

    void Update()
    {
        if(currentRecoil > 0)
        {
            currentRecoil -= Time.deltaTime * 200;
            if(currentRecoil < 0)
                currentRecoil = 0;

            transform.parent.localRotation = Quaternion.Euler(-currentRecoil,0,0);
        }
    }
    public void Shoot()
    {
        if(CanShoot())
        {
            gunMuzzle.Play();
            gunShot.Play();
            sound.Play();
            currentAmmo--;

            RaycastHit hit;
            if(Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                if(hit.transform.gameObject.TryGetComponent<Destructible>(out Destructible dTarget))
                {
                    if(dTarget.bCanHit)
                        hitMarker.HitTarget(dTarget.TakeDamage(damage, hit));
                }
                else if(hit.transform.gameObject.TryGetComponent<TargetControl>(out TargetControl cTarget))
                {
                    if(!cTarget.bActive)
                        hitMarker.HitTarget(cTarget.Hit());
                }
            }

            gunHUD.SetCount(currentAmmo,clipSize);
            StartCoroutine("Fire");
        }
    }

    public void Reload()
    {
        if(!bReloading && ammoReserve > 0 && (currentAmmo < clipSize))
            StartCoroutine("Reloading");
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.01f);
        delayTime = (float)60 / rpm;
        currentRecoil = recoil * 60;
        transform.parent.localRotation = Quaternion.Euler(-currentRecoil, 0, 0);

        if(delayTime > 0)
        {
            yield return new WaitForSeconds(delayTime);
            delayTime = 0;
        }
        
        if(bPressed && bAutomatic)
            Shoot();
    }

    public IEnumerator Reloading()
    {
        bReloading = true;
        
        transform.parent.localRotation = Quaternion.Euler(0,0,0);
        float endRotation = 360.0f;
        float t = 0.0f;
        while ( t  < reloadTime )
        {
            t += Time.deltaTime;
            float xRotation = Mathf.Lerp(0, endRotation, t / reloadTime) % 360.0f;
            transform.parent.localRotation = Quaternion.Euler(xRotation,0,0);
            yield return null;
        }

        int ammoAdd = clipSize - currentAmmo;
        if(ammoAdd > ammoReserve)
            ammoAdd = ammoReserve;
        ammoReserve -= ammoAdd;
        currentAmmo += ammoAdd;

        gunHUD.SetCount(currentAmmo, clipSize);
        gunHUD.SetReserve(ammoReserve);

        bReloading = false;
    }
}