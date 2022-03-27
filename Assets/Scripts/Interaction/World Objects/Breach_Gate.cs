using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breach_Gate : IControlManager
{
    #region Components
    [Header("Componenets")]
    public HingeJoint LeftGate;
    public HingeJoint RightGate;
    public MeshRenderer FrontLights, BackLights;
    public Material m_On, m_Off;
    Collider controlCollider;
    #endregion

    #region Triggers
    [Header("Triggers")]
    public List<ControlTrigger> FrontTriggers = new List<ControlTrigger>();
    public List<ControlTrigger> BackTriggers = new List<ControlTrigger>();
    public bool isFrontActive, isBackActive;
    #endregion
    public float angle = 90f;

    void Awake()
    {
        controlCollider = GetComponent<Collider>();
        Physics.IgnoreCollision(controlCollider, LeftGate.GetComponent<Collider>(), true);
        Physics.IgnoreCollision(controlCollider, RightGate.GetComponent<Collider>(), true);

        SetGates();
    }

    void SetGates()
    {
        foreach(var trigger in FrontTriggers)
            trigger.AssignParent(this, 0);

        foreach(var trigger in BackTriggers)
            trigger.AssignParent(this, 1);
    }

    public override void CheckStatus(int num = 0)
    {
        //0 is front, 1 is back

        if(num == 0)
        {
            isFrontActive = GetTriggerStatus(FrontTriggers);
            ToggleFront(isFrontActive);
        }
        else
        {
            isBackActive = GetTriggerStatus(BackTriggers);
            ToggleBack(isBackActive);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<PhysicsMovement>(out PhysicsMovement pm))
            Physics.IgnoreCollision(controlCollider, pm.GetComponent<Collider>(), pm.isDashing);
    }

    public void ToggleFront(bool active)
    {
        isFrontActive = active;
        var mats = FrontLights.sharedMaterials;

        if(active)
        {
            JointLimits limits = LeftGate.limits;
            limits.min = -angle;
            LeftGate.limits = limits;

            limits = RightGate.limits;
            limits.max = angle;
            RightGate.limits = limits;

            mats[1] = m_On;
        }
        else
        {
            JointLimits limits = LeftGate.limits;
            limits.min = 0;
            LeftGate.limits = limits;

            limits = RightGate.limits;
            limits.max = 0;
            RightGate.limits = limits;

            mats[1] = m_Off;
        }

        FrontLights.materials = mats;
    }

    public void ToggleBack(bool active)
    {
        isBackActive = active;
        var mats = BackLights.sharedMaterials;

        if(active)
        {
            JointLimits limits = LeftGate.limits;
            limits.max = angle;
            LeftGate.limits = limits;

            limits = RightGate.limits;
            limits.min = -angle;
            RightGate.limits = limits;

            mats[1] = m_On;
        }
        else
        {
            JointLimits limits = LeftGate.limits;
            limits.max = 0;
            LeftGate.limits = limits;

            limits = RightGate.limits;
            limits.min = 0;
            RightGate.limits = limits;

            mats[1] = m_Off;
        }

        BackLights.materials = mats;
    }

    void OnValidate()
    {
        ToggleFront(isFrontActive);
        ToggleBack(isBackActive);
    }
}
