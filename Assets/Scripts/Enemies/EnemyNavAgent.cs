using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_STATE
{
    PATROL, IDLE, CHASE, ATTACK, FORCED
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavAgent : MonoBehaviour
{
    [SerializeField] EnemyAI AI;
    [SerializeField] Transform LookDirection;
    NavMeshAgent agent;
    GameObject player;
    Rigidbody rb;

    Vector3 offsetFromRB;

    [Header("Layer Masks")]
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    #region Patroling
    Vector3 walkPoint;
    bool bSetWalkPoint;
    [Header("Behavior Controls")]
    public float walkPointRange;
    #endregion

    #region States

    AI_STATE aiState = AI_STATE.IDLE;
    public float maxIdleTime;
    bool bPlayerInSightRange, bPlayerInAttackRange, bForceApplied;
    bool bIdle, bCanSee = true;
    #endregion

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if(AI != null)
        {
            rb = AI.gameObject.GetComponent<Rigidbody>();
            player = AI.player;

            AI.enemyAgent = this;
            offsetFromRB = rb.position - transform.position;
        }

        maxIdleTime = Mathf.Max(1f, maxIdleTime);
    }

    //
    // AI State behavior credit to yt/"Dave / GameDevelopment"
    //
    void Update()
    {
        if(AI == null || !AI.bAlive || aiState == AI_STATE.FORCED)
            return;

        bPlayerInSightRange = Physics.CheckSphere(transform.position, AI.sightRange, whatIsPlayer);
        bPlayerInAttackRange = Physics.CheckSphere(transform.position, AI.attackRange, whatIsPlayer);

        // Simple State Machine
        if(!bPlayerInSightRange && !bPlayerInAttackRange)
            Patroling();
        else if(bPlayerInSightRange && !bPlayerInAttackRange)
            ChasePlayer();
        else if(bPlayerInSightRange && bPlayerInAttackRange)
            AttackPlayer();

        if(rb != null && AI != null)
        {
            Vector3 pos = agent.nextPosition;
            pos.y = rb.position.y;
            rb.MovePosition(pos);

            if(aiState == AI_STATE.FORCED)
            //if(aiState == AI_STATE.ATTACK  || aiState == AI_STATE.FORCED)
                return;

            Vector3 direction = LookDirection.position;
            direction.y = rb.position.y;
            rb.transform.LookAt(direction);
        }
    }

#region Patroling State
    protected void Patroling()
    {
        if(bIdle)
            return;

        aiState = AI_STATE.PATROL;

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
        aiState = AI_STATE.IDLE;
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
        aiState = AI_STATE.CHASE;
        agent.SetDestination(player.transform.position);
    }
#endregion

#region Attack State
    protected void AttackPlayer()
    {
        aiState = AI_STATE.ATTACK;
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        AI.TryToAttack();
    }
#endregion

#region Forced State
    public void ForceApplied()
    {
        aiState = AI_STATE.FORCED;
        CancelInvoke("CheckForGrounded");
        InvokeRepeating("CheckForGrounded", 1f, 0.1f);
    }

    public void CheckForGrounded()
    {
        if(rb.velocity.magnitude > 0)
            return;

        transform.position = rb.position - offsetFromRB;
        aiState = AI_STATE.IDLE;
        CancelInvoke("CheckForGrounded");
    }
#endregion
}
