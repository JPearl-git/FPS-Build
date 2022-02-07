using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentTest : MonoBehaviour
{
    [SerializeField] Transform Target;
    NavMeshAgent agent;

    public LayerMask whatIsGround, whatIsPlayer;

#region Patroling
    public Vector3 walkPoint;
    bool bSetWalkPoint;
    public float walkPointRange;
#endregion

#region Attacking
    public float timeBtwnAttack;
    bool bHasAttacked;
#endregion

#region States
    public float sightRange, attackRange;
    public bool bPlayerInSightRange, bPlayerInAttackRange;
#endregion

    protected void Awake()
    {
        //base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }

//
// AI State behavior credit to yt/"Dave / GameDevelopment"
//
    protected void Update()
    {
        //if(!bAlive)
        //    return;

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
        if(!bSetWalkPoint)
            SearchWalkPoint();

        if(bSetWalkPoint)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
            bSetWalkPoint = false;
    }

    protected void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = transform.position + new Vector3(randomX,0, randomZ);

        bSetWalkPoint = Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround);
    }
#endregion

#region Chase State
    protected void ChasePlayer()
    {
        agent.SetDestination(Target.position);
    }
#endregion

#region Attack State
    protected void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        //LookAtTarget(Target.position);
        transform.LookAt(Target.position);

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
