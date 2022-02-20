using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IButtonInteract : MonoBehaviour
{
    public bool bCanInteract;
    protected InputManager manager;

    public virtual void Interact()
    {
        bCanInteract = false;
        manager.RemoveInteractive(this);
    }

    protected virtual void CallForInteract()
    {
        if(!bCanInteract)
            return;

        manager.SetInteract(this);
    }

    protected bool CheckForPlayer(Collider other)
    {
        if(!other.tag.Equals("Player"))
            return false;
;
        if(other.TryGetComponent<InputManager>(out manager))
            return true;

        return false;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(CheckForPlayer(other))
            InvokeRepeating("CallForInteract", 0f, 0.1f);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if(CheckForPlayer(other))
        {
            CancelInvoke("CallForInteract");
            manager.RemoveInteractive(this);
        }
    }
}
