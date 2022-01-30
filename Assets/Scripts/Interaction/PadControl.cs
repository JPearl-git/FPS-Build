using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadControl : ControlTrigger
{
    List<GameObject> objsOnPad = new List<GameObject>();
    Transform pad;
    [SerializeField] Material M_On, M_Off;

    public bool bStayActive;

    void Start()
    {
        pad = transform.GetChild(0);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Entity"))
        {
            if(!objsOnPad.Contains(other.gameObject))
            {
                objsOnPad.Add(other.gameObject);
                CheckObjects();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if((other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Entity")) && !bStayActive)
        {
            if(objsOnPad.Contains(other.gameObject))
            {
                objsOnPad.Remove(other.gameObject);
                CheckObjects();
            }
        }
    }

    void CheckObjects()
    {
        if(bActive && objsOnPad.Count == 0)
        {
            bActive = false;
            StopAllCoroutines();
            pad.gameObject.GetComponent<MeshRenderer>().material = M_Off;
            StartCoroutine("MovePad");
        }
        else if(!bActive && objsOnPad.Count > 0)
        {
            bActive = true;
            StopAllCoroutines();
            pad.gameObject.GetComponent<MeshRenderer>().material = M_On;
            StartCoroutine("MovePad");
        }

        NotifyParent();
    }

    IEnumerator MovePad()
    {
        var fromPos = pad.localPosition;
        var toPos = new Vector3(fromPos.x, 1.4f, fromPos.z);
        if(bActive)
            toPos.y = 0.7f;

        for(var t = 0f; t < 1; t += Time.deltaTime/0.5f) {
            pad.localPosition = Vector3.Lerp(fromPos,toPos,t);
            yield return null;
        }
    }
}
