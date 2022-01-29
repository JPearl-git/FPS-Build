using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : ControlTrigger
{
    [SerializeField] Material m_Actice;

    public HitMarkerType Hit()
    {
        if(bActive)
            return HitMarkerType.None;
        
        bActive = true;
        gameObject.GetComponent<MeshRenderer>().material = m_Actice;
        NotifyParent();
        return HitMarkerType.Normal;
    }
}
