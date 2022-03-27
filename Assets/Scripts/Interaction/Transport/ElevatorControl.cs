using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElevatorControl : IControlManager
{
    Transform LeftDoor, RightDoor;
    ControlTrigger InteriorButton;
    int currentFloor = 0, currentIndex;
    [Tooltip("Direction that elevator initailly goes")]
    public bool isGoingDown;

    [Header("Elevator Door Variables")]
    [Tooltip("Are the Elevator doors open")]
    [SerializeField] bool bOpenDoors;
    [Tooltip("Triggers to open Elevator doors")]
    [SerializeField] List<ControlTrigger> DoorAccessTriggers = new List<ControlTrigger>();
    
    [SerializeField] float doorOpenZ = 0.9f;
    bool isMoving;

    [Header("Other Floors")]
    public List<ElevatorCall> FloorTriggers = new List<ElevatorCall>();
    
    #region Awake Functions
    void Awake()
    {
        LeftDoor = transform.Find("Left Door");
        RightDoor = transform.Find("Right Door");
        InteriorButton = gameObject.GetComponentInChildren<ControlTrigger>();

        InteriorButton.AssignParent(this, 0, true);
        
        OrganizeFloors();

        StartCoroutine(MoveDoors(bOpenDoors));
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

        if(!bOpenDoors && DoorAccessTriggers.Count > 0)
            AssignDoorAccess();
    }

    void AssignDoorAccess()
    {
        foreach(var trigger in DoorAccessTriggers)
            trigger.AssignParent(this, 0);
    }
    #endregion

    #region Trigger Status Functions
    public override void CheckStatus(int num = 0)
    {
        // Open Elevator doors
        if(num == 0)
        {
            bool bActive = GetTriggerStatus(DoorAccessTriggers);
            if(bActive)
                StartCoroutine(MoveDoors(true));
        }
        // Move Elevator to Specific floor
        else
        {
            ElevatorCall floor = FloorTriggers.Where(x => x.FloorLevel == num).FirstOrDefault();
            bool bActive = GetTriggerStatus(floor.triggers);

            if(bActive && currentFloor != floor.FloorLevel && !isMoving)
                GoToFloor(floor);
        }
    }

    /*
    bool GetTriggerStatus(List<ControlTrigger> triggers)
    {
        for (int i = 0; i < triggers.Count; i++)
        {
            if(!triggers[i].bActive)
            {
                return false;
            }
        }

        return true;
    }
    */
    #endregion

    #region Interior Button Functions
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
    #endregion

    void GoToFloor(ElevatorCall floor)
    {
        Vector3 newPos = transform.position;
        newPos.y = floor.FloorHeight;

        currentFloor = floor.FloorLevel;
        currentIndex = FloorTriggers.IndexOf(floor);

        StopAllCoroutines();
        StartCoroutine(MoveElevator(transform.position, newPos));
    }

    #region Coroutines
    IEnumerator MoveElevator(Vector3 currentPos, Vector3 newPos)
    {
        StartCoroutine(MoveDoors(false));

        isMoving = true;
        yield return new WaitForSeconds(1f);

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

        yield return new WaitForSeconds(1f);
        StartCoroutine(MoveDoors(true));
    }

    IEnumerator MoveDoors(bool state)
    {
        bOpenDoors = state;
        float endZ = 0f;

        if(bOpenDoors)
            endZ = doorOpenZ;
        
        Vector3 newLeft = LeftDoor.localPosition;
        Vector3 currentLeft = LeftDoor.localPosition;
        newLeft.z = endZ;

        while(Mathf.Abs(LeftDoor.localPosition.z - endZ) > 0.1f)
        {
            currentLeft = Vector3.MoveTowards(LeftDoor.localPosition, newLeft, 0.03f);
            LeftDoor.localPosition = currentLeft;

            RightDoor.localPosition = new Vector3(RightDoor.localPosition.x, RightDoor.localPosition.y, -currentLeft.z);
            yield return new WaitForSeconds(0.01f);
        }

        LeftDoor.localPosition = newLeft;
        newLeft.z *= -1;
        RightDoor.localPosition = newLeft;
    }
    #endregion

    #region ElevatorCall Struct
    [Serializable]
    public struct ElevatorCall
    {
        [Tooltip("Optional: All triggers to call elevator to specific floor")]
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


