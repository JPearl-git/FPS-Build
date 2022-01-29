using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_UI : MonoBehaviour
{
    public Slider HealthBar;
    public GameObject DeathFade;

    public void SetHealth(float value)
    {
        HealthBar.value = value;
    }

    public void PlayDeathFade()
    {
        DeathFade.SetActive(true);
        DeathFade.GetComponent<Animation>().Play();
    }
}
