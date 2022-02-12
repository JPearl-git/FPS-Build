using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : BotStats
{
    [Header("Canvas")]
    [SerializeField] Transform Canvas;

    [HideInInspector] public EnemyNavAgent enemyAgent;

    SubCollider[] subColliders;

    #region Attacking
    Transform muzzle;
    public float timeBtwnAttack;
    public float sightRange, attackRange;
    [Range(1f,90f)] public float shootAngle;
    bool bHasAttacked, bReloading;
    #endregion

    protected void Start()
    {
        base.Start();

        if(gunScript != null)
            muzzle = gunScript.muzzle.transform;

        subColliders = transform.GetComponentsInChildren<SubCollider>();
    }

    void Update()
    {
        if(!bAlive)
            return;

        if(Canvas != null)
            Canvas.LookAt(player.transform);
    }

#region Attack State
    protected override void LookAtTarget(Vector3 target)
    {
        //var bodyTarget = target;
        //bodyTarget.y = transform.position.y;
        //transform.rotation = SmoothRotation(bodyTarget, transform, 1);

        if(Head != null)
        {
            var headTarget = target;
            headTarget.y += 0.8f;
            Head.rotation = SmoothRotation(headTarget, Head, 1);
        }

        if(Gun != null)
            MoveHand(target);
    }

    protected override void MoveHand(Vector3 target)
    {
        WeaponHand.rotation = SmoothRotation(target, WeaponHand, 1f);
        bCanFire = true;
    }

    Quaternion SmoothRotation(Vector3 target, Transform current, float smoothing)
    {
        Vector3 dir = target - current.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 smoothRotation = Quaternion.Lerp(current.rotation, lookRotation, Time.deltaTime).eulerAngles;
        return Quaternion.Euler(smoothRotation);
    }

    public void TryToAttack()
    {
        LookAtTarget(player.transform.position);

        if(bHasAttacked || gunScript == null)
            return;

        TargetInRange();

        if(gunScript.currentAmmo > 0)
        {
            timeBtwnAttack = (float)60 / gunScript.rpm;
            Invoke(nameof(ResetAttack), timeBtwnAttack);
        }
        else if(!bReloading)
        {
            bReloading = true;
            gunScript.Reload();
            Invoke("Reload", gunScript.reloadTime);
        }
    }

    protected void TargetInRange()
    {
        float angle = Vector3.Angle(WeaponHand.forward, (player.transform.position - transform.position).normalized);
        if(angle < shootAngle)
            Fire();
        else if(Physics.Raycast(WeaponHand.position, WeaponHand.forward, out RaycastHit hit))
        {
            if(hit.collider.gameObject.tag.Equals("Player"))
                Fire();
        }
    }

    protected override void Fire()
    {
        if(!bCanFire || !pStats.bAlive || !bAlive)
            return;

        if(!gunScript.CanShoot() || bReloading)
            return;

        gunScript.Shoot();
        bHasAttacked = true;
    }

    protected override void Reload()
    {
        bReloading = false;
    }

    protected void ResetAttack()
    {
        bHasAttacked = false;
    }
#endregion

    public override HitMarkerType GetHit(int damage, RaycastHit hit, bool bCritHit = false)
    {
        if(!bCritHit)
            bCritHit = CheckForCrit(hit.point);
        return base.GetHit(damage, hit, bCritHit);
    }

    protected bool CheckForCrit(Vector3 hitPoint)
    {
        for (int i = 0; i < subColliders.Length; i++)
        {
            SubCollider sub = subColliders[i];
            if(sub.TryGetComponent<Collider>(out Collider collider))
            {
                if(collider.bounds.Contains(hitPoint) && sub.bCritical)
                    return true;
            }
        }

        return false;
    }

    void Death()
    {
        base.Death();
    }

#region Gizmos
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3f);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawRay(new Ray(WeaponHand.position, Quaternion.Euler(0, shootAngle, 0).eulerAngles));
    }
#endregion
}
