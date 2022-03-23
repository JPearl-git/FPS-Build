using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter_Remains : MonoBehaviour
{
    public float lifeTime = 8f;
    public void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
