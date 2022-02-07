using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : BotStats
{
    [Header("Canvas")]
    [SerializeField] Transform Canvas;
    NavMeshAgent agent;

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
    public float timeBtwnAttack;
    bool bHasAttacked;
    #endregion

    #region States
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

//
// AI State behavior credit to yt/"Dave / GameDevelopment"
//
    void Update()
    {
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
    protected void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        LookAtTarget(player.transform.position);

        if(!bHasAttacked)
        {
            //Attack code here
            Debug.Log("Attack!");

            bHasAttacked = true;
            Invoke(nameof(ResetAttack), timeBtwnAttack);
        }
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
    }
#endregion
}
