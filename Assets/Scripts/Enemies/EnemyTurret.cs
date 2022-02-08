using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Destructible
{
    Transform Target;
    HealthStatus healthStatus;
    public float range = 15f;
    bool bTargetInRange;

    [SerializeField] Transform Turret, BarrelPivot;
    [SerializeField] GameObject explosion, smoke, sparks;

    void Awake()
    {
        Target = GameObject.Find("Player").transform;
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

    void UpdateTarget()
    {
        if(Target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, Target.position);
            bTargetInRange = distanceToTarget <= range;
        }
        else
            bTargetInRange = false;
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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

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
        Debug.Log("Health is low");
        healthStatus = HealthStatus.LOW;

        //Spawn sparks
    }

    void VeryDamagedState()
    {
        Debug.Log("Health is very low");
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawLine(Turret.position, Turret.position + Turret.forward);
        Gizmos.DrawLine(Turret.position, Turret.position + Turret.up);
        Gizmos.DrawLine(BarrelPivot.position, BarrelPivot.position + BarrelPivot.forward);
    }
}
