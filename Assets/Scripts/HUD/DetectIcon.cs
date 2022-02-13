using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DetectIcon : MonoBehaviour
{
    [SerializeField] Sprite NoSpotIcon, CautionIcon, DetectionIcon;

    Image image;

    void Awake()
    {
        image = GetComponent<Image>();

        if(NoSpotIcon != null)
            image.sprite = NoSpotIcon;
    }

    public void ChangeIcon(AWARENESS state)
    {
        switch(state)
        {
            case AWARENESS.NO_DETECTION:
            {
                if(NoSpotIcon != null)
                    image.sprite = NoSpotIcon;
                break;
            }
            case AWARENESS.CAUTIOUS:
            {
                if(CautionIcon != null)
                    image.sprite = CautionIcon;
                break;
            }
            case AWARENESS.DETECTED:
            {
                if(DetectionIcon != null)
                    image.sprite = DetectionIcon;
                break;
            }
            default:
                break;
        }
    }
}
