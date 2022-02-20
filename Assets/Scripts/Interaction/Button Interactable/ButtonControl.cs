using System;
using UnityEngine;

[RequireComponent(typeof(ControlTrigger))]
public class ButtonControl : IButtonInteract
{
    [SerializeField] GameObject buttonObject;
    [SerializeField] Material activeMaterial;
    ControlTrigger controlTrigger;

    public Vector3 localEndPoint;
    Vector3 localStartPoint, destination;
    bool bReverse;

    void Awake()
    {
        controlTrigger = GetComponent<ControlTrigger>();
        localStartPoint = buttonObject.transform.localPosition;
    }

    public override void Interact()
    {
        base.Interact();
        controlTrigger.bActive = true;
        controlTrigger.NotifyParent();

        if(buttonObject.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
            meshRenderer.material = activeMaterial;

        destination = localEndPoint;
        InvokeRepeating("AnimateButton", 0f, 0.1f);
    }

    void AnimateButton()
    {
        Vector3 currentLocal = buttonObject.transform.localPosition;
        if((currentLocal - destination).magnitude < 0.01f)
        {
            if(bReverse)
                CancelInvoke("AnimateButton");
            else
            {
                bReverse = true;
                destination = localStartPoint;
            }
        }

        Vector3 newLocal = Vector3.Lerp(currentLocal, destination, 0.3f);
        buttonObject.transform.localPosition = newLocal;
    }
}
