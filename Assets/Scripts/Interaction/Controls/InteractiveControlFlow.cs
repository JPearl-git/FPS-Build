using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveControlFlow : MonoBehaviour
{
    public List<ControlTrigger> controls = new List<ControlTrigger>();
    public List<ControllableSwitch> ToggleTargets = new List<ControllableSwitch>();
    bool bOperational, bActive;

    // Start is called before the first frame update
    void Start()
    {
        if(controls.Count < 1 || ToggleTargets.Count < 1)
            return;

        bOperational = true;
        for(int i = 0; i < controls.Count; i++)
            controls[i].parent = this;

        SetTargetStates();
    }

    public void CheckStatus()
    {
        if(!bOperational)
            return;

        bActive = CheckControls();

        SetTargetStates();
        //toggleTarget.bActive = bActive;
    }

    void SetTargetStates()
    {
        foreach(ControllableSwitch target in ToggleTargets)
        {
            if(target.toggleTarget != null)
                target.toggleTarget.bActive = bActive ^ target.ReverseActivation;
        }
    }

    bool CheckControls()
    {
        for(int i = 0; i < controls.Count; i++)
        {
            if(!controls[i].bActive)
                return false;
        }

        return true;
    }
}

[Serializable]
public struct ControllableSwitch
{
    public ISwitchable toggleTarget;
    public bool ReverseActivation;
}
