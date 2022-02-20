using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_UI : MonoBehaviour
{
    public Slider HealthBar;
    public GameObject DeathFade;
    [Header("Interactive Elements")]
    public GameObject InteractPanel;
    public Text InteractText;

    public void SetHealth(float value)
    {
        HealthBar.value = value;
    }

    public void PlayDeathFade()
    {
        DeathFade.SetActive(true);
        DeathFade.GetComponent<Animation>().Play();
    }

    #region Interact Prompt Functions
    public void ShowInteractPrompt()
    {
        if(InteractPanel == null)
            return;

        InteractPanel.SetActive(true);
    }

    public void HideInteractPrompt()
    {
        if(InteractPanel == null)
            return;

        InteractPanel.SetActive(false);
    }
    #endregion
}
