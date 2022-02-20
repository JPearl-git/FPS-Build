using System;
using UnityEngine;

[RequireComponent(typeof(ControlTrigger))]
public class ButtonControl : IButtonInteract
{
    ControlTrigger controlTrigger;
    [SerializeField] GameObject buttonObject;

    [Header("Active State")]
    [SerializeField] Material activeMaterial;
    public Vector3 localEndPoint;

    [Header("Deactive State")]
    [SerializeField] Material inactiveMaterial;
    public bool autoDepress;
    Vector3 localStartPoint, destination;
    bool bReverse;

    void Awake()
    {
        controlTrigger = GetComponent<ControlTrigger>();
        localStartPoint = buttonObject.transform.localPosition;
    }

    public override void Interact()
    {
        Debug.Log("Interact");
        if(!bCanInteract)
            return;
            
        base.Interact();
        controlTrigger.bActive = true;
        controlTrigger.NotifyParent();

        if(buttonObject.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
            meshRenderer.material = activeMaterial;

        destination = localEndPoint;
        InvokeRepeating("AnimateButton", 0f, 0.1f);
    }

    public void Deactivate()
    {
        bCanInteract = true;
        controlTrigger.bActive = false;
        controlTrigger.NotifyParent();

         if(buttonObject.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
            meshRenderer.material = inactiveMaterial;
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

                if(autoDepress)
                    Deactivate();
            }
        }

        Vector3 newLocal = Vector3.Lerp(currentLocal, destination, 0.3f);
        buttonObject.transform.localPosition = newLocal;
    }
}
