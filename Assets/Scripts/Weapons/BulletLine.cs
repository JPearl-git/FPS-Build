using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BulletLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Destroy(gameObject, .1f);
    }

    public void SetMaterial(Material material)
    {
        lineRenderer.material = material;
    }

}
