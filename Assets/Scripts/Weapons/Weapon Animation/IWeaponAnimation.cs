using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IWeaponAnimation : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    protected int targetPosition, currentPosition = -1;
    protected float speed;
    protected bool bCanChange = true;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void EndReload()
    {
        animator.SetBool("isReloading", false);
    }

    public void Reset()
    {
        animator.SetBool("isSwinging", false);
    }

    public void AnimateAttack()
    {
        animator.SetBool("isSwinging", true);
    }

    protected virtual void EndMeleeAttack(){}
}
