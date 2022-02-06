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
        //if(Target != null)
        //    Aim();
        if(!bTargetInRange)
            return;

        Aim();
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
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = lookRotation.eulerAngles;
        Vector3 SmoothRotation = Quaternion.Lerp(Turret.rotation, lookRotation, Time.deltaTime).eulerAngles;

        Turret.localRotation = Quaternion.Euler(0, rotation.y, 0);
        BarrelPivot.rotation = Quaternion.Euler(rotation.x, SmoothRotation.y, initalEulerRotation.z);
    }

    void TestAim()
    {
        // Working
        Vector3 dir = Target.position - BarrelPivot.position;
        //Quaternion lookRotation = Quaternion.LookRotation(dir);

        // Testing
        Vector3 TurretBaseDir = Vector3.ProjectOnPlane(Target.position - Turret.position, Turret.up);
        //Vector3 dir = Vector3.ProjectOnPlane(Target.position - BarrelPivot.position, Turret.up);
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Quaternion lookRotation = Quaternion.LookRotation(TurretBaseDir);

        Vector3 rotation = lookRotation.eulerAngles;

        // Working
        Vector3 SmoothRotation = Quaternion.Lerp(Turret.rotation, lookRotation, Time.deltaTime).eulerAngles;
        //Turret.rotation = Quaternion.Euler(0, rotation.y, 0);

        // Testing
        //Turret.rotation = Quaternion.Euler(0, SmoothRotation.y, 0);
        Turret.localRotation = Quaternion.Euler(0, rotation.y, 0);
        //Turret.rotation = Quaternion.Euler(initalEulerRotation + new Vector3(0, SmoothRotation.y, 0));

        // Working
        BarrelPivot.rotation = Quaternion.Euler(rotation.x, SmoothRotation.y, initalEulerRotation.z);

        //Vector3 Test = Vector3.ProjectOnPlane(Target.position - Turret.position, Turret.up);
        //Debug.DrawLine(Turret.position, Turret.position + Test, Color.green, 0.1f);
        //Debug.DrawLine(Turret.position, Turret.position + Turret.up, Color.blue, 0.1f);
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
