using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class ISwitchable : MonoBehaviour, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
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

    public abstract void Activate();
    public abstract void Deactivate();
}
