using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class BotStats : Destructible
{
    #region Components
    protected Rigidbody rb;
    protected GameObject player;
    protected PlayerStats pStats;
    protected GameObject Gun;
    protected Gun gunScript;
    #endregion

    #region Attached Parts
    [Header("Attached Parts")]
    [SerializeField] protected Transform Head;
    [SerializeField] protected Transform WeaponHand;
    [SerializeField] protected Slider slider;
    #endregion
 
    [Header("Optional")][SerializeField] GameObject GunPrefab;

    protected bool bCanHitPlayer, bCanFire;
    protected float hitDelay = 3f;

#region Unity Basics
    protected void Awake()
    {
        player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        pStats = player.GetComponent<PlayerStats>();
    }

    protected void Start()
    {
        base.Start();

        if(GunPrefab == null)
            return;
        
        InstantiateGun(GunPrefab);
        //CancelInvoke("")
    }
#endregion

#region Stat Methods
public void InstantiateGun(GameObject prefab)
    {
        if(Gun != null)
            Destroy(Gun);

        Gun = Instantiate(prefab, WeaponHand);
        if(Gun.TryGetComponent<Gun>(out gunScript))
        {
            gunScript.bPressed = true;
            gunScript.bAutomatic = false;
            //InvokeRepeating("Fire", 0, (float)60 / gunScript.rpm);
        }
    }

   public void UpdateHealth()
    {
        if(slider != null)
        {
            float percent = (float)health / maxHealth;
            slider.value = percent;
        }
    }
#endregion

#region AI Behavior
    public override void TakeDamage(int damage, Vector3 hitDirection, bool bCritHit = false)
    {
        base.TakeDamage(damage, hitDirection, bCritHit);
        UpdateHealth();
    }

    protected virtual void LookAtTarget(Vector3 target)
    {
        var bodyTarget = target;
        bodyTarget.y = transform.position.y;
        transform.LookAt(bodyTarget);

        if(Head != null)
        {
            var headTarget = target;
            headTarget.y += 0.8f;
            Head.LookAt(headTarget);
        }

        if(Gun != null)
            MoveHand(target);
    }

    protected virtual void MoveHand(Vector3 target)
    {
        WeaponHand.LookAt(target);
        bCanFire = true;
    }

    protected virtual void Fire()
    {
        if(!bCanFire || !pStats.bAlive || !bAlive)
            return;

        if(gunScript.currentAmmo < 1)
        {
            Reload();
            return;
        }

        if(!gunScript.CanShoot())
            return;

        gunScript.Shoot();
    }

    protected virtual void Reload()
    {
        Debug.Log("Reloading");
        CancelInvoke("Fire");

        gunScript.Reload();
        InvokeRepeating("Fire", gunScript.reloadTime, (float)60 / gunScript.rpm);
    }
    
    protected override void Death()
    {
        CancelInvoke();

        rb.freezeRotation = false;

        if(slider != null)
            slider.gameObject.SetActive(false);

        rb.freezeRotation = false;
        Vector3 direction = transform.forward.normalized * -1;
        rb.AddForce(direction * 200);

        Destroy(gameObject, 3f);
    }
#endregion

#region Collision Detection
    protected void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.Equals(player))
        {
            bCanHitPlayer = true;
            InvokeRepeating("HitPlayer",0,5f);
        }
    }

    protected void OnCollisionExit(Collision other)
    {
        if(other.gameObject.Equals(player))
            bCanHitPlayer = false;
    }

    protected void HitPlayer()
    {
        if(bCanHitPlayer)
            pStats.TakeDamage(5);
        else
            CancelInvoke("HitPlayer");
    }
#endregion
}
