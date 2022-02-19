using System.Collections.Generic;
using UnityEngine;

public class Objective_Control : MonoBehaviour
{
    [SerializeField] List<IObjective> Objectives = new List<IObjective>();

    IObjective currentObjective;
    int objective_number = 0;
    HUD_Waypoint hud;
    
    void Awake()
    {
        hud = GameObject.Find("HUD").GetComponent<HUD_Waypoint>();

        if(hud == null)
            return;

        for (int i = 0; i < Objectives.Count; i++)
            Objectives[i].Initialize_Objective(this, i);
    }

    void Start()
    {
        if(Objectives.Count > 0)
            SetObjective(Objectives[0]);
    }

    public void SetObjective(IObjective objective)
    {
        currentObjective = objective;
        currentObjective.Current_Objective();
        
        if(hud == null)
            return;

        hud.ObjectiveText.text = objective.objectiveText;

        if(objective.bLocation)
            hud.SetTarget(objective.transform.position);
        else
            hud.RemoveTarget();
    }

    public void NextObjective()
    {
        objective_number ++;

        if(objective_number < Objectives.Count)
            SetObjective(Objectives[objective_number]);
        else if(hud != null)
            hud.RemoveTarget();
    }
}
