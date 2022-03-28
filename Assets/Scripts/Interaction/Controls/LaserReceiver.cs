using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LaserReceiver : ControlTrigger
{
    public Collider ReceiveTrigger;
    [Tooltip("For speific beam reception")]
    public int channel = 0;

    public void RecievedSignal()
    {
        if(!bActive)
        {
            bActive = true;
            NotifyParent();
        }
    }

    public void LoseSignal()
    {
        bActive = false;
        NotifyParent();
    }
}
