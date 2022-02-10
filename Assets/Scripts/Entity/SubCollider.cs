using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SubCollider : MonoBehaviour
{
    public GameObject ParentObject;
    public bool bCritical;

    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.magenta);
    }
}
