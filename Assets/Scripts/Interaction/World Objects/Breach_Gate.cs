using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breach_Gate : IControlManager
{
    public HingeJoint LeftGate, RightGate;
    public MeshRenderer FrontLights, BackLights;
    public Material m_On, m_Off;
    Collider controlCollider;

    public bool isFrontActive, isBackActive;
    public float angle = 90f;

    void Awake()
    {
        //SetGate(LeftGate, 1);
        //SetGate(RightGate, -1);
        controlCollider = GetComponent<Collider>();
        Physics.IgnoreCollision(controlCollider, LeftGate.GetComponent<Collider>(), true);
        Physics.IgnoreCollision(controlCollider, RightGate.GetComponent<Collider>(), true);
    }

    void Update()
    {

    }

    void SetGate(GameObject gate, int zAxis)
    {
        if(gate == null)
            return;

        ControlTrigger ct = gate.AddComponent<ControlTrigger>();
        ct.parent = this;
    }

    public override void CheckStatus(int num = 0)
    {
        throw new NotImplementedException();
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
