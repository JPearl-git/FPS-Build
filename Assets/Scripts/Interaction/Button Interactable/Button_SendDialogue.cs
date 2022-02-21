using UnityEngine;

[RequireComponent(typeof(Call_Dialogue))]
public class Button_SendDialogue : IButtonInteract
{
    Call_Dialogue call_Dialogue;
    public float reactivateTime = 3f;

    void Awake()
    {
        TryGetComponent<Call_Dialogue>(out call_Dialogue);
    }

    void ReActivate()
    {
        bCanInteract = true;
    }

    public override void Interact()
    {
        if(!bCanInteract)
            return;

        base.Interact();
        if(call_Dialogue != null)
        {
            bCanInteract = !call_Dialogue.Call();
            if(call_Dialogue.HasMessages())
                Invoke("ReActivate", reactivateTime);
        }
    }
}
