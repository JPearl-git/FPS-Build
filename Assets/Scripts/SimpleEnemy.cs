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

    protected override void Death()
    {
        slider.gameObject.SetActive(false);
        rb.freezeRotation = false;
        Vector3 direction = transform.forward.normalized * -1;
        rb.AddForce(direction * 200);

        Destroy(gameObject, 3f);
    }

    public override void TakeDamage(int damage)
    {
        if(slider != null)
        {
            float percent = (float)health / maxHealth;
            slider.value = percent;
        }
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
            bCanHitPlayer = true;
            StartCoroutine("HitPlayer");
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.Equals(player))
            bCanHitPlayer = false;
    }

    IEnumerator HitPlayer()
    {
        if(bCanHitPlayer)
        {
            pStats.TakeDamage(5);
            yield return new WaitForSeconds(hitDelay);
            yield return HitPlayer();
        }
        else
            yield return null;
    }
}
