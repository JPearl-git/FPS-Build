using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : IWeapon
{
    [Header("Gun Details")]
    [Range(0f,1f)] public float recoil;
    public int currentAmmo, clipSize, ammoReserve;
    public ParticleSystem gunMuzzle;

    float currentRecoil = 0;
    bool bReloading;

    //void Start()
    //{
    //    hitMarker = GameObject.Find("HUD").GetComponent<HitMarker>();
    //
    //    var control = GameObject.Find("Level Control");
    //    if(control.TryGetComponent<DetectionNotice>(out DetectionNotice notice))
    //        detectionNotice = notice;
    //}

    public override void Initialize(GunHUD gHUD, DetectionNotice detectionNotice)
    {
        base.Initialize(gHUD, detectionNotice);
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
    public override void Attack()
    {
        if(!CanShoot())
            return;

        gunMuzzle.Play();
        sound.Play();
        currentAmmo--;

        RaycastHit hit;
        Ray ray = new Ray(gunMuzzle.transform.position, gunMuzzle.transform.forward);

        if(Physics.Raycast(ray, out hit))
        {
            GameObject HitTarget = hit.transform.gameObject;
            bool bCritHit = false;

            if(HitTarget.TryGetComponent<SubCollider>(out SubCollider sub))
            {
                HitTarget = sub.ParentObject;
                bCritHit = sub.bCritical;
                Debug.Log("Critical " + bCritHit);
            }

            // Damage Entity Types
            if(HitTarget.TryGetComponent<EntityStats>(out EntityStats entity))
            {
                //Debug.Log("Hit " + hit.transform.gameObject.name);
                if(HitTarget.TryGetComponent<Destructible>(out Destructible dTarget))
                {
                    if(dTarget.bCanHit)
                        hitMarker.HitTarget(dTarget.GetHit(damage, hit));
                }
                else if(entity.bAlive)
                    entity.TakeDamage(damage, hit.normal, bCritHit);
            }

            // Hit non-entity Targets
            else if(HitTarget.TryGetComponent<TargetControl>(out TargetControl cTarget))
            {
                if(!cTarget.bActive)
                    hitMarker.HitTarget(cTarget.Hit());
            }
        }

        if(gunHUD != null)
            gunHUD.SetCount(currentAmmo,clipSize);

        StartCoroutine("Fire");

        if(detectionNotice != null)
            detectionNotice.CallDetectors(this.transform.position);
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
            delayTime = 0f;
        }
        
        if(bPressed && bAutomatic)
            Attack();
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

        if(gunHUD != null)
        {
            gunHUD.SetCount(currentAmmo, clipSize);
            gunHUD.SetReserve(ammoReserve);
        }

        bReloading = false;
    }

    public void CancelActions()
    {
        StopAllCoroutines();
        bReloading = false;
        transform.parent.localRotation = Quaternion.Euler(0,0,0);
        delayTime = 0f;
    }
}
