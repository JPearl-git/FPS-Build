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
        gAmmoCount.text = current + "/" + clip;
    }

    public void SetReserve(int reserve)
    {
        gReserves.text = reserve.ToString();
    }
}
