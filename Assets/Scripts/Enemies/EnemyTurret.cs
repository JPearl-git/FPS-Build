using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyTurret : Detection
{
    public float range = 15f, fireDelay = 2.5f;
    bool bTargetInRange, bFire;

    #region Components
    [Header("Componenets")]
    [SerializeField] ParticleSystem[] Blasters;
    [SerializeField] Transform Turret, BarrelPivot;
    Transform Target;
    HealthStatus healthStatus;
    AudioSource audioSource;
    #endregion

    #region Particle Effects
    [Header("Particle Effect Prefabs")]
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject electricity;
    #endregion

    void Awake()
    {
        Target = GameObject.Find("Player").transform;
        audioSource = GetComponent<AudioSource>();
        healthStatus = HealthStatus.FULL;
    }
    
    void Start()
    {
        base.Start();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if(!bTargetInRange)
            return;

        Aim();
        //TestAim();
    }

    #region Targeting Methods

    void UpdateTarget()
    {
        if(Target == null)
        {
            bTargetInRange = false;
            bFire = false;
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, Target.position);
        bTargetInRange = distanceToTarget <= range;

        Vector3 targetDirection = (Target.position - BarrelPivot.position);
        float angle = Vector3.Angle(targetDirection, BarrelPivot.forward);
        
        if(angle < 10)
            TargetInSight();
        else
            bFire = false;
    }

    // Works with Grounded Turret
    void Aim()
    {
        Vector3 dir = Target.position - BarrelPivot.position;
        Vector3 TurretBaseDir = Vector3.ProjectOnPlane(Target.position - Turret.position, Turret.up);
        
        Quaternion TurretLookRotation = Quaternion.LookRotation(TurretBaseDir, Turret.up);
        Quaternion BarrelLookRotation = Quaternion.LookRotation(dir, Turret.up);

        Vector3 SmoothTurretRotation = Quaternion.Lerp(Turret.rotation, TurretLookRotation, Time.deltaTime).eulerAngles;
        Vector3 BarrelRotation = Quaternion.Lerp(BarrelPivot.rotation, BarrelLookRotation, Time.deltaTime).eulerAngles;
        
        Turret.rotation = Quaternion.Euler(SmoothTurretRotation);
        BarrelPivot.rotation = Quaternion.Euler(BarrelRotation);
    }

    // Testing
    void TestAim()
    {
        Vector3 dir = Target.position - BarrelPivot.position;
        Vector3 TurretBaseDir = Vector3.ProjectOnPlane(Target.position - Turret.position, Turret.up);
        
        Quaternion TurretLookRotation = Quaternion.LookRotation(TurretBaseDir, Turret.up);
        Quaternion BarrelLookRotation = Quaternion.LookRotation(dir, Turret.up);

        Vector3 SmoothTurretRotation = Quaternion.Lerp(Turret.rotation, TurretLookRotation, Time.deltaTime).eulerAngles;
        Vector3 BarrelRotation = Quaternion.Lerp(BarrelPivot.rotation, BarrelLookRotation, Time.deltaTime).eulerAngles;
        
        Turret.rotation = Quaternion.Euler(SmoothTurretRotation);
        BarrelPivot.rotation = Quaternion.Euler(BarrelRotation);
    }

    void TargetInSight()
    {
        //float distanceFromTarget = (Target.position - BarrelPivot.position).magnitude;
        
        //Debug.Log("Angle from " + gameObject.name + " = " + angle);
        if(!bTargetInRange || bFire)
            return;

        bFire = true;
        InvokeRepeating("Fire", 0, fireDelay);
    }

    void Fire()
    {
        if(!bFire)
        {
            CancelInvoke("Fire");
            return;
        }

        foreach(var blaster in Blasters)
        {
            blaster.Play();

            if(Physics.Raycast(blaster.transform.position, blaster.transform.forward, out RaycastHit hit))
            {
                if(hit.collider.gameObject.TryGetComponent<PlayerStats>(out PlayerStats pStats))
                    pStats.TakeDamage(5, hit.normal);
            }
        }
        audioSource.Play();
    }
    #endregion

    #region Damage Methods

    public override void TakeDamage(int damage, bool bCritHit = false)
    {
        base.TakeDamage(damage, bCritHit);

        if(healthStatus.Equals(HealthStatus.VERY_LOW))
            return;

        float healthFraction = (float)health / maxHealth;
        if(healthStatus.Equals(HealthStatus.LOW))
        {
            if(healthFraction < 0.3f)
            {
                VeryDamagedState();
                return;
            }
        }
        else if(healthFraction < 0.6f)
            DamagedState();
    }

    void DamagedState()
    {
        healthStatus = HealthStatus.LOW;
        
        var arcObj = Instantiate(electricity, transform.position, transform.rotation, transform);
        if(arcObj.TryGetComponent<ParticleSystem>(out ParticleSystem pArc))
            pArc.Play();
    }

    void VeryDamagedState()
    {
        healthStatus = HealthStatus.VERY_LOW;

        var pSmoke = Instantiate(smoke, transform.position + transform.up * 0.5f, transform.rotation, transform);
        if(pSmoke.TryGetComponent<ParticleSystem>(out ParticleSystem ps))
            ps.Play();
    }

    protected override void Death()
    {
        var exp = Instantiate(explosion, transform.position, transform.rotation);
        exp.transform.localScale = new Vector3(1f,1f,1f);
        exp.GetComponent<Explosion>().Init(2, 2, 2, StatusEffect.BURN, 2f);
        Destroy(gameObject,0.1f);
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawLine(Turret.position, Turret.position + Turret.up);
        Gizmos.DrawLine(BarrelPivot.position, BarrelPivot.position + BarrelPivot.forward);
    }
}
