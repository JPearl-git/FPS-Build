using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AWARENESS
{
    NO_DETECTION, CAUTIOUS, DETECTED
}

public class Detection : Destructible
{
    [HideInInspector] public AWARENESS detectState;
    [HideInInspector] public Vector3 detectTarget, currentTarget;
    [HideInInspector] public bool bPlayerInDetectRange, bPlayerInSightRange, bPlayerInAttackRange, bAtDetectTarget;

    public LayerMask whatIsPlayer;

    [Header("Detection Ranges")]
    public float detectionRange = 25;
    public float sightRange = 15;
    public float attackRange = 5;

    void OnValidate()
    {
        if(detectionRange < 0)
            detectionRange = 1;

        if(sightRange < 0)
            sightRange = 1;

        if(attackRange < 0)
            attackRange = 1;
    }

    public void CheckRanges()
    {
        bPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        bPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        bPlayerInDetectRange = Physics.CheckSphere(transform.position, detectionRange, whatIsPlayer);

        if(detectState != AWARENESS.DETECTED && (bPlayerInAttackRange || bPlayerInAttackRange))
            detectState = AWARENESS.DETECTED;

        else if(detectState == AWARENESS.DETECTED && !bPlayerInDetectRange)
        {
            detectState = AWARENESS.CAUTIOUS;
            SetDetectTarget(currentTarget);
        }
    }

    public void SetDetectTarget(Vector3 position)
    {
        bAtDetectTarget = false;
        
        if(detectState == AWARENESS.DETECTED)
            return;

        detectTarget = position;
    }

    #region Gizmos
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    #endregion
}
