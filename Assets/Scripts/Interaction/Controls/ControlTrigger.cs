using UnityEngine;

public class ControlTrigger : MonoBehaviour
{
    [HideInInspector] public IControlManager parent;

    [HideInInspector] public bool bActive;
    [HideInInspector] public int num = 0;
    public void NotifyParent()
    {
        if(parent != null)
            parent.CheckStatus(num);
    }

    public void AssignParent(IControlManager manager, int n)
    {
        parent = manager;
        num = n;
    }
}
