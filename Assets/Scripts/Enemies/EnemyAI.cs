using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : BotStats
{
    [Header("Canvas")]
    [SerializeField] Transform Canvas;

    [Header("Layer Masks")]
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    #region Patroling
    Vector3 walkPoint;
    bool bSetWalkPoint;
    [Header("Behavior Controls")]
    public float walkPointRange;
    #endregion

    #region Attacking
    Transform muzzle;
    public float timeBtwnAttack;
    [Range(1f,90f)] public float shootAngle;
    bool bHasAttacked, bReloading;
    #endregion

    #region States
    NavMeshAgent agent;
    public float sightRange, attackRange, maxIdleTime;
    bool bPlayerInSightRange, bPlayerInAttackRange;
    bool bIdle, bCanSee = true;
    #endregion

    protected void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        maxIdleTime = Mathf.Max(1f, maxIdleTime);
    }

    protected void Start()
    {
        base.Start();
        muzzle = gunScript.muzzle.transform;
    }

//
// AI State behavior credit to yt/"Dave / GameDevelopment"
//
    void Update()
    {
        //Debug.DrawLine(muzzle.position, muzzle.position + muzzle.forward, Color.magenta);
        //Debug.DrawLine(muzzle.position, muzzle.position + (player.transform.position - transform.position).normalized, Color.gray);
        //Debug.Log("Angle = " + Vector3.Angle(muzzle.forward, (player.transform.position - transform.position).normalized));

        if(!bAlive)
            return;

        if(Canvas != null)
            Canvas.LookAt(player.transform);

        bPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        bPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Simple State Machine
        if(!bPlayerInSightRange && !bPlayerInAttackRange)
            Patroling();
        else if(bPlayerInSightRange && !bPlayerInAttackRange)
            ChasePlayer();
        else if(bPlayerInSightRange && bPlayerInAttackRange)
            AttackPlayer();
    }

#region Patroling State
    protected void Patroling()
    {
        if(bIdle)
            return;

        if(!bSetWalkPoint)
            SearchWalkPoint();

        if(bSetWalkPoint)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            bSetWalkPoint = false;
            Idle();
        }
    }

    protected void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = transform.position + new Vector3(randomX,0, randomZ);

        bSetWalkPoint = Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround);
    }
#endregion

#region Idle State
    protected void Idle()
    {
        bIdle = true;
        Invoke("EndIdle", Random.Range(1f, maxIdleTime));
    }

    protected void EndIdle()
    {
        bIdle = false;
    }
#endregion

#region Chase State
    protected void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }
#endregion

#region Attack State
    protected override void LookAtTarget(Vector3 target)
    {
        var bodyTarget = target;
        bodyTarget.y = transform.position.y;
        //transform.LookAt(bodyTarget);
        //transform.LookAt(Vector3.Lerp(transform.position, bodyTarget, Time.deltaTime / 10000));
        transform.rotation = SmoothRotation(bodyTarget, transform, 1);

        if(Head != null)
        {
            var headTarget = target;
            headTarget.y += 0.8f;
            //Head.LookAt(headTarget);
            Head.rotation = SmoothRotation(headTarget, Head, 1);
        }

        if(Gun != null)
            MoveHand(target);
    }

    protected override void MoveHand(Vector3 target)
    {
        //WeaponHand.LookAt(target);
        //WeaponHand.LookAt(Vector3.Lerp(WeaponHand.forward, target, 1));
        WeaponHand.rotation = SmoothRotation(target, WeaponHand, 3f);
        bCanFire = true;
    }

    Quaternion SmoothRotation(Vector3 target, Transform current, float smoothing)
    {
        Vector3 dir = target - current.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 smoothRotation = Quaternion.Lerp(current.rotation, lookRotation, Time.deltaTime).eulerAngles;
        return Quaternion.Euler(smoothRotation);
    }

    protected void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
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
