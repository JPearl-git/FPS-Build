using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Location_Objective : IObjective
{
    protected override void OnInitialize()
    {
        bLocation = true;
    }
    public override void Current_Objective()
    {
        base.Current_Objective();
    }

    public override void Objective_Complete()
    {
        base.Objective_Complete();
    }

    void OnTriggerEnter(Collider other)
    {
        if(!status.Equals(Objective_Status.CURRENT))
            return;

        if(other.tag.Equals("Player"))
            Objective_Complete();
    }
}
