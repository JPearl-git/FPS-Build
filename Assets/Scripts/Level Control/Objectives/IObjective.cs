using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Objective_Status
{
    NOT_ACTIVE, ACTIVE, CURRENT, COMPLETE
}
public class IObjective : MonoBehaviour
{
    protected Objective_Control objective_Control;
    protected int objective_number;
    protected Objective_Status status = Objective_Status.NOT_ACTIVE;
    [HideInInspector] public bool bLocation;
    public string objectiveText;

    public void Initialize_Objective(Objective_Control objective_Control, int num)
    {
        this.objective_Control = objective_Control;
        objective_number = num;
        status = Objective_Status.ACTIVE;
        OnInitialize();

        if(num == 0)
            Current_Objective();
    }

    public virtual void Current_Objective()
    {
        status = Objective_Status.CURRENT;
    }

    public virtual void Objective_Complete()
    {
        status = Objective_Status.COMPLETE;

        objective_Control.NextObjective();
    }

    protected virtual void OnInitialize(){}
}
