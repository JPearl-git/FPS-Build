using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class SimpleEnemy : Destructible
{
    Rigidbody rb;
    GameObject player;
    PlayerStats pStats;
    public Slider slider;
    public float speed = 5f;
    bool bCanHitPlayer;
    float hitDelay = 3f;

    public override void Initialize()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        pStats = player.GetComponent<PlayerStats>();
    }

    public override void Death()
    {
        slider.gameObject.SetActive(false);
        StartCoroutine("Die");
    }

    public override void GetHit(int damage)
    {
        if(slider != null)
        {
            float percent = (float)health / maxHealth;
            slider.value = percent;
        }
    }

    IEnumerator Die()
    {
        rb.freezeRotation = false;
        //Vector3 direction = Vector3.Normalize(transform.InverseTransformPoint(lastHit.point));
        Vector3 direction = transform.forward.normalized * -1;
        rb.AddForce(direction * 200);

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void Update()
    {
        if(health > 0)
            MoveToTarget();
    }

    void MoveToTarget()
    {
        if(player != null)
        {
            if(pStats.bAlive)
            {
                var target = player.transform.position;
                float change = Time.deltaTime * speed;

                transform.LookAt(target);
                float distance = Vector3.Distance(target, transform.position);
                transform.position += transform.forward * change;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.Equals(player))
        {
            bCanHit = true;
            StartCoroutine("HitPlayer");
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.Equals(player))
            bCanHit = false;
    }

    IEnumerator HitPlayer()
    {
        if(bCanHit)
        {
            pStats.TakeDamage(5);
            yield return new WaitForSeconds(hitDelay);
            yield return HitPlayer();
        }
        else
            yield return null;
    }
}
