using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTrigger : MonoBehaviour
{
    [HideInInspector] public bool bActive;

    [HideInInspector] public InteractiveControlFlow parent;

    public void NotifyParent()
    {
        if(parent != null)
            parent.CheckStatus();
    }
}
