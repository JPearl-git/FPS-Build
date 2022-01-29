using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitMarkerType
{
    None,
    Normal,
    Critical,
    Kill
}

public class HitMarker : MonoBehaviour
{
    public GameObject HMobj;
    public Material HMmat;
    public float hitMarkerDuration = .1f;
    float currentHMduration = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHMduration > 0)
        {
            currentHMduration -= Time.deltaTime;
            if(currentHMduration <= 0)
                HMobj.SetActive(false);
        }
    }

    public void HitTarget(HitMarkerType type)
    {
        switch(type)
        {
            case HitMarkerType.Normal:
                HMmat.color = Color.white;
                break;
            case HitMarkerType.Critical:
                HMmat.color = Color.yellow;
                break;
            case HitMarkerType.Kill:
                HMmat.color = Color.red;
                break;
            default:
                HMmat.color = Color.white;
                break;
        }
        if(type != HitMarkerType.None)
        {
            currentHMduration = hitMarkerDuration;
            HMobj.SetActive(true);
        }
    }
}
