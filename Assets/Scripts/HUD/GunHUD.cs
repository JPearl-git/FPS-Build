using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunHUD : MonoBehaviour
{
    public Text gName, gAmmoCount, gReserves;
    public void SetName(string name)
    {
        gName.text = name;
    }

    public void SetCount(int current, int clip)
    {
        if(clip < 0)
            gAmmoCount.text = "---/---";
        else
            gAmmoCount.text = current + "/" + clip;
    }

    public void SetReserve(int reserve)
    {
        if(reserve < 0)
            gReserves.text = "---";
        else
            gReserves.text = reserve.ToString();
    }
}
