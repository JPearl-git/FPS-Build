using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveControlFlow : IControlManager
{
    [Tooltip("The Controls for providing power")]
    public List<ControlTrigger> controls = new List<ControlTrigger>();
    [Tooltip("Each Toggle Target contains a ISwitchable and a reverse power option")]
    public List<ControllableSwitch> ToggleTargets = new List<ControllableSwitch>();
    bool bOperational, bActive;

    // Start is called before the first frame update
    void Start()
    {
        if(controls.Count < 1 || ToggleTargets.Count < 1)
            return;

        bOperational = true;
        for(int i = 0; i < controls.Count; i++)
            controls[i].AssignParent(this, 0);

        SetTargetStates();
    }

    public override void CheckStatus(int num = 0)
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

    #region ControllableSwitch Struct
    [Serializable]
    public struct ControllableSwitch
    {
        public ISwitchable toggleTarget;
        public bool ReverseActivation;
    }
    #endregion
}


