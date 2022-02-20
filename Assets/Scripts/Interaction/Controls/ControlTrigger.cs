using UnityEngine;

public class ControlTrigger : MonoBehaviour
{
    [HideInInspector] public IControlManager parent;

    [HideInInspector] public bool bActive, isSwitable;
    [HideInInspector] public int position = 0;
    public void NotifyParent()
    {
        if(parent != null)
        {
            if(isSwitable)
                parent.TurnSwitch(bActive);
            else
                parent.CheckStatus(position);
        }
    }

    public void AssignParent(IControlManager manager, int num, bool isSwitable = false)
    {
        parent = manager;
        position = num;
        this.isSwitable = isSwitable;
    }
}
