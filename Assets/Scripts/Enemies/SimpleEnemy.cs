using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : BotStats
{
    public float speed = 5f;

    void Update()
    {
        EnemyUpdate();
    }

    protected virtual void EnemyUpdate()
    {
        if(bAlive)
            MoveToTarget();
    }

    protected void MoveToTarget()
    {
        if(player == null)
            return;

        if(!pStats.bAlive)
            return;

        var target = player.transform.position;

        float distance = Vector3.Distance(target, transform.position);
        float change = Time.deltaTime * speed;
        transform.position += transform.forward * change;

        LookAtTarget(target);
    }
}
