using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class TestAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animation anim = GetComponent<Animation>();
        anim.Play();
        Debug.Log("Playing " + anim.clip.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
