using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Kill_Objective : IObjective
{
    [SerializeField] List<GameObject> enemyObjects = new List<GameObject>();
    List<Detection> enemies = new List<Detection>();
    int lastCheckSize = 0, initialCount;

    #region IObjective Override Functions
    protected override void OnInitialize()
    {
        if(enemyObjects.Count < 1)
            return;

        for (int i = 0; i < enemyObjects.Count;)
        {
            Detection enemy = enemyObjects[i].GetComponentInChildren<Detection>();
            if(enemy == null)
            {
                enemyObjects.Remove(enemyObjects[i]);
                continue;
            }

            enemies.Add(enemy);
            i++;
        }

        initialCount = enemies.Count;
        lastCheckSize = initialCount;
        SetObjectiveText();
    }

    public override void Current_Objective()
    {
        base.Current_Objective();
        InvokeRepeating("CheckForUpdate", 0f, 0.1f);
    }

    public override void Objective_Complete()
    {
        base.Objective_Complete();
        CancelInvoke("CheckForUpdate");
    }
    #endregion

    void SetObjectiveText()
    {
        objectiveText = "Eliminate Enemies\nEnemies Remaining: " + lastCheckSize + "/" + initialCount;
        objective_Control.UpdateObjectiveText(objectiveText);
    }

    void CheckForUpdate()
    {
        enemies = enemies.Where(x => x != null && x.bAlive).ToList();

        if(lastCheckSize == enemies.Count)
            return;

        if(enemies.Count < 1)
        {
            Objective_Complete();
            return;
        }

        lastCheckSize = enemies.Count;
        SetObjectiveText();
    }
}
