using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveControlFlow : MonoBehaviour
{
    public List<ControlTrigger> controls = new List<ControlTrigger>();
    public DoorControl doorTarget;
    bool bOperational, bActive;

    // Start is called before the first frame update
    void Start()
    {
        if(controls.Count > 0 && doorTarget != null)
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
            if(bActive)
                doorTarget.OpenDoor();
            else
                doorTarget.CloseDoor();
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
