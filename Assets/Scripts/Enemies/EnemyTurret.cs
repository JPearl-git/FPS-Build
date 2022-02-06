using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : EntityStats
{
    Transform Target;
    Vector3 initalEulerRotation;
    public float range = 15f;
    bool bTargetInRange;

    [SerializeField] Transform Turret, BarrelPivot;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        initalEulerRotation = transform.rotation.eulerAngles;
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

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Quaternion TurretLookRotation = Quaternion.LookRotation(TurretBaseDir, Turret.up);

        Vector3 rotation = lookRotation.eulerAngles;
        Vector3 SmoothRotation = Quaternion.Lerp(Turret.rotation, TurretLookRotation, Time.deltaTime).eulerAngles;
        
        Turret.rotation = Quaternion.Euler(SmoothRotation);
        BarrelPivot.rotation = Quaternion.Euler(rotation.x, SmoothRotation.y, SmoothRotation.z);
    }

    // Testing
    void TestAim()
    {
        Vector3 dir = Target.position - BarrelPivot.position;
        Vector3 TurretBaseDir = Vector3.ProjectOnPlane(Target.position - Turret.position, Turret.up);

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Quaternion TurretLookRotation = Quaternion.LookRotation(TurretBaseDir, Turret.up);

        Vector3 rotation = lookRotation.eulerAngles;
        Vector3 SmoothRotation = Quaternion.Lerp(Turret.rotation, TurretLookRotation, Time.deltaTime).eulerAngles;
        
        Turret.rotation = Quaternion.Euler(SmoothRotation);
        BarrelPivot.rotation = Quaternion.Euler(rotation.x, SmoothRotation.y, SmoothRotation.z);

        Vector3 Test = Vector3.ProjectOnPlane(Target.position - Turret.position, Turret.up);
        Debug.DrawLine(Turret.position, Turret.position + dir, Color.magenta, 0.1f);
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
