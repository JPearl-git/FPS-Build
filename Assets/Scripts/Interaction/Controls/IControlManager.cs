using System.Collections.Generic;
using UnityEngine;

public abstract class IControlManager : MonoBehaviour
{
    public abstract void CheckStatus(int num = 0);

    public virtual void TurnSwitch(bool isActive){}

    protected virtual bool GetTriggerStatus(List<ControlTrigger> triggers)
    {
        for (int i = 0; i < triggers.Count; i++)
        {
            if(!triggers[i].bActive)
            {
                return false;
            }
        }

        return true;
    }
}
