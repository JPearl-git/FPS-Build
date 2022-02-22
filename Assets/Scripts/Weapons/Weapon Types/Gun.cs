using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : IWeapon
{
    [HideInInspector] public RecoilControl recoilControl;

    [Header("Gun Details")]
    [Range(1,1000)]public int rpm = 100;
    public int currentAmmo, clipSize, ammoReserve;
    [Range(.1f,10f)]public float reloadSpeed = 1;
    
    public bool bAutomatic;
    public ParticleSystem gunMuzzle;
    bool bReloading;

    [Header("Recoil Details")]
    public Vector3 recoilVector = new Vector3(-2,2,0.35f);
    public float snapWeight = 10;
    public float returnSpeed = 5;

    #region Core Functions
    public override void Equip(GunHUD gHUD, DetectionNotice detectionNotice, GunSlot gunSlot)
    {
        base.Equip(gHUD, detectionNotice, gunSlot);
        gunHUD.SetCount(currentAmmo, clipSize);
        gunHUD.SetReserve(ammoReserve);

        if(weaponAnimation != null)
            weaponAnimation.animator.SetFloat("reloadSpeed", reloadSpeed);

        recoilControl = gunSlot.recoilControl;
        recoilControl.SetRecoil(recoilVector, snapWeight, returnSpeed);
    }

    public bool CanShoot()
    {
        if(delayTime > 0) return false;
        if(bReloading) return false;
        if(currentAmmo < 1) return false;

        return bPressed;
    }
    #endregion
    
    #region Fire Gun Functions

    // Override of IWeapon Attack()
    public override void Attack()
    {
        if(!CanShoot())
            return;

        currentAmmo--;
        sound.Play();
        HitScan(gunMuzzle);

        if(gunHUD != null)
            gunHUD.SetCount(currentAmmo,clipSize);

        StartCoroutine("Fire");

        if(detectionNotice != null)
            detectionNotice.CallDetectors(this.transform.position);
    }

    // Project hitscan ray from muzzle
    protected virtual void HitScan(ParticleSystem muzzle)
    {
        muzzle.Play();

        Ray ray = new Ray(muzzle.transform.position, muzzle.transform.forward);

        if(Physics.Raycast(ray, out RaycastHit hit))
            HitTarget(hit);
    }

    // Determine if a potential target is hit and what to do
    protected virtual void HitTarget(RaycastHit hit)
    {
        GameObject HitTarget = hit.transform.gameObject;
        bool bCritHit = false;

        if(HitTarget.TryGetComponent<SubCollider>(out SubCollider sub))
        {
            HitTarget = sub.ParentObject;
            bCritHit = sub.bCritical;
            //Debug.Log("Critical " + bCritHit);
        }

        // Damage Entity Types
        if(HitTarget.TryGetComponent<EntityStats>(out EntityStats entity))
        {
            //Debug.Log("Hit " + hit.transform.gameObject.name);
            if(HitTarget.TryGetComponent<Destructible>(out Destructible dTarget))
            {
                if(dTarget.bCanHit)
                    hitMarker.HitTarget(dTarget.GetHit(damage, hit.normal, hit.point));
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

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.01f);
        delayTime = (float)60 / rpm;
        //currentRecoil = recoil * 60;
        //transform.parent.localRotation = Quaternion.Euler(-currentRecoil, 0, 0);
        if(recoilControl != null)
            recoilControl.RecoilFire();

        if(delayTime > 0)
        {
            yield return new WaitForSeconds(delayTime);
            delayTime = 0f;
        }
        
        if(bPressed && bAutomatic)
            Attack();
    }
    #endregion

    #region Reload Gun Functions
    public void Reload()
    {
        if(weaponAnimation == null)
            return;

        if(!bReloading && ammoReserve > 0 && (currentAmmo < clipSize))
        {
            bReloading = true;
            weaponAnimation.animator.SetBool("isReloading", true);
        }
    }

    public void EndReload(bool bComplete)
    {
        bReloading = false;

        if(!bComplete)
            return;

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
    }
    #endregion

    #region Unequip Gun Functions
    public override void Unequip()
    {
        base.Unequip();
        bReloading = false;
        delayTime = 0f;
    }
    #endregion

    // Not yet Implemented
    public override void AIEquip(){}
    
}
