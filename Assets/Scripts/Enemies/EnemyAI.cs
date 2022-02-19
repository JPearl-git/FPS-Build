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

    [Header("AI Variables")]
    public float timeBtwnAttack;
    [Range(1f,90f)] public float shootAngle;
    bool bHasAttacked;
    #endregion

    protected void Start()
    {
        base.Start();

        if(gunScript != null)
            muzzle = gunScript.gunMuzzle.transform;

        subColliders = transform.GetComponentsInChildren<SubCollider>();
    }

    void Update()
    {
        if(!bAlive)
            return;

        CheckRanges();

        if(Canvas != null)
            Canvas.LookAt(player.transform);
    }

    #region Attack State
    public override void LookAtTarget(Vector3 target, bool moveHead = true)
    {
        var bodyTarget = target;
        bodyTarget.y = transform.position.y;
        transform.rotation = SmoothRotation(bodyTarget, transform, 3);

        if(Head != null && moveHead)
        {
            if(moveHead)
            {
                var headTarget = target;
                headTarget.y += 0.8f;
                Head.rotation = SmoothRotation(headTarget, Head, 3);
            }
            else
            {
                var headTarget = target;
                headTarget.y = Head.position.y;
                Head.rotation = SmoothRotation(headTarget, Head, 3);
            }
            
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
        if(dir.Equals(Vector3.zero))
            return Quaternion.identity;
        
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 smoothRotation = Quaternion.Lerp(current.rotation, lookRotation, Time.deltaTime * smoothing).eulerAngles;
        return Quaternion.Euler(smoothRotation);
    }

    public void TryToAttack()
    {
        //LookAtTarget(player.transform.position);

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
            //Invoke("Reload", gunScript.reloadSpeed);
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

        gunScript.Attack();
        bHasAttacked = true;
    }

    //protected override void Reload()
    //{
    //    bReloading = false;
    //}

    protected void ResetAttack()
    {
        bHasAttacked = false;
    }
    #endregion

    #region Damage Methods
    public override HitMarkerType GetHit(int damage, Vector3 hitDirection, Vector3 hitLocation, bool bCritHit = false)
    {
        if(!bCritHit)
            bCritHit = CheckForCrit(hitLocation);
        return base.GetHit(damage, hitDirection, hitLocation, bCritHit);
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

    protected override void Death()
    {
        Destroy(detectIcon.gameObject);
        base.Death();
    }
    #endregion
}
