using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class ISwitchable : MonoBehaviour
{
    [SerializeField]
    private bool bStartState;

    private bool _bActive;
    public bool bActive
    {
        get { return _bActive; }
        set
        { 
            if(_bActive != value)
            {
                _bActive = value;
                if(_bActive)
                    Activate();
                else
                    Deactivate();
            }
        }
    }

    protected void Start()
    {
        bActive = bStartState;
    }

    public abstract void Activate();
    public abstract void Deactivate();
}
