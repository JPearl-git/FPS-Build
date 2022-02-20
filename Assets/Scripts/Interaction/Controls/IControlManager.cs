using UnityEngine;

public abstract class IControlManager : MonoBehaviour
{
    public abstract void CheckStatus(int num = 0);

    public virtual void TurnSwitch(bool isActive){}
}
