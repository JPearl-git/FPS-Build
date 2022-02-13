using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_STATE
{
    PATROL, IDLE, CHASE, ATTACK, FORCED, DEAD
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
    //bool bPlayerInSightRange, bPlayerInAttackRange;
    bool bForceApplied;
    bool bIdle, bCanSee = true;
    #endregion

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if(AI != null)
        {
            rb = AI.gameObject.GetComponent<Rigidbody>();

            if(AI.player == null)
                player = GameObject.Find("Player");
            else
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
        if(!AI.bAlive && aiState != AI_STATE.DEAD)
        {
            aiState = AI_STATE.DEAD;
            //Destroy(transform.parent.gameObject, 0.5f);
        }

        if(AI == null || aiState == AI_STATE.DEAD || aiState == AI_STATE.FORCED)
            return;

        // Simple State Machine
        if(!AI.bPlayerInSightRange && !AI.bPlayerInAttackRange)
            Patroling();
        else if(AI.bPlayerInSightRange && !AI.bPlayerInAttackRange)
            ChasePlayer();
        else if(AI.bPlayerInSightRange && AI.bPlayerInAttackRange)
            AttackPlayer();

        if(aiState != AI_STATE.IDLE)
            bIdle = false;

        if(rb != null && AI != null)
        {
            Vector3 pos = agent.nextPosition;
            pos.y = rb.position.y;
            rb.MovePosition(pos);

            if(aiState == AI_STATE.FORCED)
            //if(aiState == AI_STATE.ATTACK  || aiState == AI_STATE.FORCED)
                return;

            //Vector3 direction = LookDirection.position;
            //direction.y = rb.position.y;
            //rb.transform.LookAt(direction);
        }
    }

#region Patroling State
    protected void Patroling()
    {
        if(bIdle)
            return;

        aiState = AI_STATE.PATROL;

        if(AI.detectState == AWARENESS.NO_DETECTION)
            FreePatrol();
        
        if(AI.detectState == AWARENESS.CAUTIOUS)
            CautiousPatrol();
    }

    protected void CautiousPatrol()
    {
        Vector3 distanceToDestination = transform.position - AI.detectTarget;
        if(distanceToDestination.magnitude < 2.5f)
        {
            bSetWalkPoint = false;
            
            aiState = AI_STATE.IDLE;
            bIdle = true;
            AI.bAtDetectTarget = true;

            Invoke("EndIdle", Random.Range(maxIdleTime, maxIdleTime + 1f));
            return;
        }

        AI.bAtDetectTarget = false;

        agent.SetDestination(AI.detectTarget);

        AI.LookAtTarget(AI.detectTarget, false);
    }

    protected void FreePatrol()
    {
        if(!bSetWalkPoint)
            SearchWalkPoint();

        if(bSetWalkPoint)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude > 0)
            AI.LookAtTarget(walkPoint, false);

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
        if(AI.detectState == AWARENESS.CAUTIOUS && AI.bAtDetectTarget)
        {
            AI.detectState = AWARENESS.NO_DETECTION;
            AI.SetIcon();
        }

        if(aiState == AI_STATE.IDLE)
            bIdle = false;
    }
#endregion

#region Chase State
    protected void ChasePlayer()
    {
        aiState = AI_STATE.CHASE;
        agent.SetDestination(player.transform.position);
        AI.LookAtTarget(transform.position + transform.forward, false);
    }
#endregion

#region Attack State
    protected void AttackPlayer()
    {
        aiState = AI_STATE.ATTACK;
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        //AI.LookAtTarget(player.transform.position);
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
