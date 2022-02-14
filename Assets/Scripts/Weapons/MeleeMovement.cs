using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(GunSlot))]
public class MeleeMovement : MonoBehaviour
{
    [SerializeField] Transform initalPoint;

    [SerializeField] Transform[] StrikePoints;

    Transform targetPoint;
    Animator animator;

    GunSlot slot;

    int targetPosition, currentPosition = -1;
    float speed;
    bool bCanChange = true;

    void Awake()
    {
        slot = GetComponent<GunSlot>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //if(bCanChange)
        //    return;
//
        //transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetPoint.rotation, speed * Time.deltaTime);
//
        //if(transform.position.Equals(targetPoint.position))
        //{
        //    currentPosition = targetPosition;
        //    bCanChange = true;
        //}
    }

    public void Reset(bool isInstant)
    {
        if(isInstant)
        {
            transform.position = initalPoint.position;
            transform.rotation = initalPoint.rotation;
        }
    }

    public void ChangePosition(float speed)
    {
        if(!bCanChange)
            return;
        
        bCanChange = false;
        this.speed = speed;

        targetPosition = currentPosition + 1;
        if(targetPosition > StrikePoints.Length - 1)
        {
            targetPosition = 0;
            currentPosition = 0;
        }

        targetPoint = StrikePoints[targetPosition];
        Debug.Log("Strikepos size = " + StrikePoints.Length);
        Debug.Log("Current = " + currentPosition + " Target = " + targetPosition);

        //InvokeRepeating("MoveWeapon", 0, 0.1f); 
    }

    void MoveWeapon()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed);
        Vector3 direction = Vector3.RotateTowards(transform.forward, targetPoint.forward, speed, 0f);
        transform.rotation = Quaternion.LookRotation(direction);

        if(transform.position.Equals(targetPoint.position))
        {
            Debug.Log("At target pos " + StrikePoints[targetPosition]);
            currentPosition = targetPosition;
            bCanChange = true;
            CancelInvoke("MoveWeapon");
        }
    }

    public void AnimateAttack()
    {
        animator.SetBool("isSwinging", true);
    }

    void EndMeleeAttack()
    {
        Debug.Log("Check Anim, " + slot.bAttackAction);
        if(slot.bAttackAction)
            return;

        animator.SetBool("isSwinging", false);
        var melee = slot.Weapon as MeleeWeapon;
        if(melee != null)
            melee.attackTrail.Stop();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(var i in StrikePoints)
        {
            Gizmos.DrawSphere(i.position, .1f);
            Gizmos.DrawRay(i.position, i.forward);
            Gizmos.DrawRay(i.position, i.up);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, .1f);
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
