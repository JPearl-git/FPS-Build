using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class SimpleEnemy : Destructible
{
    Rigidbody rb;
    GameObject player;
    PlayerStats pStats;
    public float speed = 5f;

    [Header("Attached Parts")]
    [SerializeField] Transform Head;
    [SerializeField] Transform WeaponHand;
    [SerializeField] Slider slider;

    [SerializeField] GameObject GunPrefab;

    GameObject Gun;
    Gun gunScript;
    
    bool bCanHitPlayer, bCanFire;
    float hitDelay = 3f;

    void Start()
    {
        base.Start();

        if(GunPrefab == null)
            return;
        
        Gun = Instantiate(GunPrefab, WeaponHand);
        if(Gun.TryGetComponent<Gun>(out gunScript))
        {
            gunScript.bPressed = true;
            gunScript.bAutomatic = false;
            InvokeRepeating("Fire", 0, (float)60 / gunScript.rpm);
        }
        //CancelInvoke("")
    }

    void Update()
    {
        if(health > 0)
            MoveToTarget();
    }

    public override void Initialize()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        pStats = player.GetComponent<PlayerStats>();
    }

    protected override void Death()
    {
        slider.gameObject.SetActive(false);
        rb.freezeRotation = false;
        Vector3 direction = transform.forward.normalized * -1;
        rb.AddForce(direction * 200);

        Destroy(gameObject, 3f);
    }

    public void UpdateHealth()
    {
        if(slider != null)
        {
            float percent = (float)health / maxHealth;
            slider.value = percent;
        }
    }

    public override void TakeDamage(int damage, bool bHasSource = false)
    {
        base.TakeDamage(damage, bHasSource);
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
        if(!bCanFire)
            return;

        Debug.Log("Check ammo");
        if(gunScript.currentAmmo < 1)
        {
            Reload();
            return;
        }

        if(!gunScript.CanShoot())
            return;

        Debug.Log("Fire!");
        gunScript.Shoot();
    }

    protected void Reload()
    {
        Debug.Log("Reloading");
        CancelInvoke("Fire");

        gunScript.Reload();
        InvokeRepeating("Fire", gunScript.reloadTime, (float)60 / gunScript.rpm);
    }

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
