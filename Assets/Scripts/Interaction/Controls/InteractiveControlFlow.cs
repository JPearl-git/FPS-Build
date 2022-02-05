using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveControlFlow : MonoBehaviour
{
    public List<ControlTrigger> controls = new List<ControlTrigger>();
    public ISwitchable toggleTarget;
    bool bOperational, bActive;

    // Start is called before the first frame update
    void Start()
    {
        if(controls.Count > 0 && toggleTarget != null)
        {
            bOperational = true;
            for(int i = 0; i < controls.Count; i++)
                controls[i].parent = this;
        }
    }

    public void CheckStatus()
    {
        if(bOperational)
        {
            bActive = CheckControls();
            toggleTarget.bActive = bActive;
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
