using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElevatorControl : IControlManager
{
    Transform LeftDoor, RightDoor;
    ControlTrigger InteriorButton;

    int currentFloor = 0;
    bool isGoingDown;

    public List<ElevatorCall> FloorTriggers = new List<ElevatorCall>();
    
    #region Awake Functions
    void Awake()
    {
        LeftDoor = transform.Find("Left Door");
        RightDoor = transform.Find("Right Door");
        InteriorButton = gameObject.GetComponentInChildren<ControlTrigger>();

        OrganizeFloors();
    }

    void OrganizeFloors()
    {
        // Added temporary floor for comparison
        ElevatorCall temp = new ElevatorCall(){ FloorBase = transform };
        FloorTriggers.Add(temp);

        // Organize floors by height
        FloorTriggers = FloorTriggers.OrderBy(y => y.FloorBase.position.y).ToList();
        int tempPos = FloorTriggers.IndexOf(temp);

        // Iterate through floors to assign values
        for (int i = 0; i < FloorTriggers.Count; i++)
        {
            // Skip tempo floor
            if(i == tempPos)
                continue;

            // Assign values to floor, than add back into list
            ElevatorCall floor = FloorTriggers[i];
            floor.FloorLevel = i - tempPos;
            floor.FloorHeight = floor.FloorBase.position.y;

            foreach(var trigger in floor.triggers)
                trigger.AssignParent(this, floor.FloorLevel);
            
            FloorTriggers[i] = floor;
        }

        FloorTriggers.Remove(temp);
    }
    #endregion

    public override void CheckStatus(int num = 0)
    {
        ElevatorCall floor = FloorTriggers.Where(x => x.FloorLevel == num).FirstOrDefault();
        
        bool bActive = true;
        for (int i = 0; i < floor.triggers.Count; i++)
        {
            if(!floor.triggers[i].bActive)
            {
                bActive = false;
                break;
            }
        }

        if(bActive && currentFloor != floor.FloorLevel)
            GoToFloor(floor);
    }

    void GoToFloor(ElevatorCall floor)
    {
        Debug.Log("Go to floor " + floor.FloorLevel);
        Vector3 newPos = transform.position;
        newPos.y = floor.FloorHeight;

        transform.position = newPos;
    }

    #region ElevatorCall Struct
    [Serializable]
    public struct ElevatorCall
    {
        //May not be what works. ControlTrigger relies on InteractiveControlFlow.
        //Possible solution, make ElevatorControl a child of ISwitchable and change triggers to InteractiveControlFlows.
        [Tooltip("All triggers to call elevator to specific floor")]
        public List<ControlTrigger> triggers;
        [HideInInspector] public int FloorLevel;
        [HideInInspector] public float FloorHeight;

        [Tooltip("Used to have a visible reference for the floor's height")]
        public Transform FloorBase;
    }
    #endregion
}


