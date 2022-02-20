using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElevatorControl : IControlManager
{
    Transform LeftDoor, RightDoor;
    ControlTrigger InteriorButton;

    public int currentFloor = 0, currentIndex;
    [Tooltip("Direction that elevator initailly goes")]
    public bool isGoingDown;
    bool isMoving;

    public List<ElevatorCall> FloorTriggers = new List<ElevatorCall>();
    
    #region Awake Functions
    void Awake()
    {
        LeftDoor = transform.Find("Left Door");
        RightDoor = transform.Find("Right Door");
        InteriorButton = gameObject.GetComponentInChildren<ControlTrigger>();

        InteriorButton.AssignParent(this, 0, true);
        
        OrganizeFloors();
    }

    void OrganizeFloors()
    {
        // Added temporary floor for comparison
        ElevatorCall temp = new ElevatorCall()
        { FloorBase = transform, FloorHeight = transform.position.y };

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

        //FloorTriggers.Remove(temp);
        currentIndex = tempPos;
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

        if(bActive && currentFloor != floor.FloorLevel && !isMoving)
            GoToFloor(floor);
    }

    public override void TurnSwitch(bool isActive)
    {
        if(!isActive || isMoving)
            return;

        isGoingDown = CheckFloorStatus();

        if(isGoingDown)
            GoToFloor(FloorTriggers[currentIndex - 1]);
        else
            GoToFloor(FloorTriggers[currentIndex + 1]);
    }

    bool CheckFloorStatus()
    {
        if(isGoingDown)
            return (currentFloor != FloorTriggers[0].FloorLevel);

        return (currentFloor == FloorTriggers[FloorTriggers.Count - 1].FloorLevel);
    }

    void GoToFloor(ElevatorCall floor)
    {
        Vector3 newPos = transform.position;
        newPos.y = floor.FloorHeight;

        currentFloor = floor.FloorLevel;
        currentIndex = FloorTriggers.IndexOf(floor);

        StartCoroutine(MoveElevator(transform.position, newPos));
    }

    IEnumerator MoveElevator(Vector3 currentPos, Vector3 newPos)
    {
        isMoving = true;
        while(Mathf.Abs(currentPos.y - newPos.y) > 0.1f)
        {
            //currentPos = Vector3.MoveTowards(currentPos, newPos, 0.1f);
            currentPos = Vector3.MoveTowards(currentPos, newPos, 0.03f);
            transform.position = currentPos;
            yield return new WaitForSeconds(0.01f);
        }
        
        transform.position = newPos;
        isMoving = false;

        if(InteriorButton.TryGetComponent<ButtonControl>(out ButtonControl button))
            button.Deactivate();
    }

    #region ElevatorCall Struct
    [Serializable]
    public struct ElevatorCall
    {
        [Tooltip("All triggers to call elevator to specific floor")]
        public List<ControlTrigger> triggers;
        [HideInInspector] public int FloorLevel;
        [HideInInspector] public float FloorHeight;

        [Tooltip("Used to have a visible reference for the floor's height")]
        public Transform FloorBase;
        [Tooltip("Do the triggers reset when the floor changes")]
        public bool bResetOnFloorChange;
    }
    #endregion
}


