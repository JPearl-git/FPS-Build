using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : EntityStats
{
    Transform Target;
    public float range = 15f;
    bool bTargetInRange;

    [SerializeField] Transform Turret, BarrelPivot;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawLine(Turret.position, Turret.position + Turret.forward);
        Gizmos.DrawLine(Turret.position, Turret.position + Turret.up);
        Gizmos.DrawLine(BarrelPivot.position, BarrelPivot.position + BarrelPivot.forward);
    }
}
