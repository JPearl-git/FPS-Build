using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveBox : ISwitchable
{
    public Color color;
    float fade = 0f;
    List<GameObject> children = new List<GameObject>();
    List<MeshRenderer> renderers = new List<MeshRenderer>();

    void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if(child.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
            {
                renderer.material.SetColor("_Color", color);
                renderers.Add(renderer);
            }
            
            children.Add(child);
        }
    }
    public override void Activate()
    {
        StartCoroutine("FadeOut");
    }

    public override void Deactivate()
    {

    }

    IEnumerator FadeOut()
    {
        while( fade < 1)
        {
            fade += 0.01f;
            foreach(var r in renderers)
                r.material.SetFloat("_Fade", fade);

            yield return new WaitForSeconds(0.01f);
        }

        while(children.Count > 0)
        {
            var child = children[0];
            children.RemoveAt(0);
            Destroy(child);
        }

        yield return null;
    }
}
