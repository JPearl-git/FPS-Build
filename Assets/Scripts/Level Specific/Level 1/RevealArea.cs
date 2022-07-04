using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealArea : MonoBehaviour
{
    public void Reveal()
    {
        foreach(Transform child in transform)
            child.gameObject.SetActive(true);
    }
}
