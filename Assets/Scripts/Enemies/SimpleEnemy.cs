using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class SimpleEnemy : Destructible
{

#region Components
    Rigidbody rb;
    GameObject player;
    PlayerStats pStats;
    GameObject Gun;
    Gun gunScript;
#endregion

    public float speed = 5f;

#region Attached Parts
    [Header("Attached Parts")]
    [SerializeField] Transform Head;
    [SerializeField] Transform WeaponHand;
    [SerializeField] Slider slider;
#endregion
 
    [Header("Optional")][SerializeField] GameObject GunPrefab;

    bool bCanHitPlayer, bCanFire;
    float hitDelay = 3f;

    void Start()
    {
        base.Start();

        if(GunPrefab == null)
            return;
        
        InstantiateGun(GunPrefab);
        //CancelInvoke("")
    }

    void Update()
    {
        if(health > 0)
            MoveToTarget();
    }

    public void InstantiateGun(GameObject prefab)
    {
        if(Gun != null)
            Destroy(Gun);

        Gun = Instantiate(prefab, WeaponHand);
        if(Gun.TryGetComponent<Gun>(out gunScript))
        {
            gunScript.bPressed = true;
            gunScript.bAutomatic = false;
            InvokeRepeating("Fire", 0, (float)60 / gunScript.rpm);
        }
    }

    public override void Initialize()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        pStats = player.GetComponent<PlayerStats>();
        base.Initialize();
    }

   public void UpdateHealth()
    {
        if(slider != null)
        {
            float percent = (float)health / maxHealth;
            slider.value = percent;
        }
    }

#region AI Behavior
    public override void TakeDamage(int damage, Vector3 hitDirection)
    {
        base.TakeDamage(damage, hitDirection);
        UpdateHealth();
    }

    protected void MoveToTarget()
    {
        if(player == null)
            return;

        if(!pStats.bAlive)
            return;

        var target = player.transform.position;

        var bodyTarget = target;
        bodyTarget.y = transform.position.y;
        transform.LookAt(bodyTarget);

        float distance = Vector3.Distance(target, transform.position);
        float change = Time.deltaTime * speed;
        transform.position += transform.forward * change;

        if(Head != null)
        {
            var headTarget = target;
            headTarget.y += 0.8f;
            Head.LookAt(headTarget);
        }

        if(Gun != null)
            MoveHand(target);
    }

    protected void MoveHand(Vector3 target)
    {
        WeaponHand.LookAt(target);
        bCanFire = true;
    }

    protected void Fire()
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

    protected void Reload()
    {
        Debug.Log("Reloading");
        CancelInvoke("Fire");

        gunScript.Reload();
        InvokeRepeating("Fire", gunScript.reloadTime, (float)60 / gunScript.rpm);
    }
    
    protected override void Death()
    {
        CancelInvoke();

        slider.gameObject.SetActive(false);
        rb.freezeRotation = false;
        Vector3 direction = transform.forward.normalized * -1;
        rb.AddForce(direction * 200);

        Destroy(gameObject, 3f);
    }
#endregion

#region Collision Detection
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.Equals(player))
        {
            bCanHitPlayer = true;
            StartCoroutine("HitPlayer");
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.Equals(player))
            bCanHitPlayer = false;
    }
#endregion
    IEnumerator HitPlayer()
    {
        if(bCanHitPlayer)
        {
            pStats.TakeDamage(5);
            yield return new WaitForSeconds(hitDelay);
            yield return HitPlayer();
        }
        else
            yield return null;
    }
}
