using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealArea : MonoBehaviour
{
    Material sMaterial;
    List<Material> childrenMaterials = new List<Material>();

    float dissolveAmount = 1f;
    void Start()
    {
        foreach(Transform child in transform)
        {
            var obj = child.gameObject;

            Material[] materials = obj.GetComponent<MeshRenderer>().materials;
            sMaterial = new Material(materials[0]);
            materials[0] = sMaterial;
            obj.GetComponent<MeshRenderer>().materials = materials;

            sMaterial.SetFloat("_DissolveAmount", dissolveAmount);
            sMaterial.SetFloat("_DissolveWidth", .05f);
            childrenMaterials.Add(sMaterial);

            obj.GetComponent<Collider>().enabled = false;
        }
    }
    public void Reveal()
    {
        InvokeRepeating("FadeIn", 0, 0.01f);
        foreach(Transform child in transform)
        {
            var obj = child.gameObject;
            obj.GetComponent<Collider>().enabled = true;
        }
    }

    void FadeIn()
    {
        dissolveAmount = Mathf.Clamp01(dissolveAmount - 0.01f);
        foreach(var mat in childrenMaterials)
            mat.SetFloat("_DissolveAmount", dissolveAmount);

        if(dissolveAmount == 0f)
            CancelInvoke("FadeIn");
    }
}
