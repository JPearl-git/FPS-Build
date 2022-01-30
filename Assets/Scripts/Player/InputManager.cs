using UnityEngine;

public class InputManager : MonoBehaviour
{
    Movement movementScript;
    PhysicsMovement pMovementScript;
    [SerializeField] PlayerLook playerLook;

    InputControls controls;
    InputControls.PlayerActions playerControl;

    Vector2 movement;
    Vector2 lookInput;

    [HideInInspector] public bool bCanControl;
    bool bPhysics;

    void Awake()
    {
        controls = new InputControls();
        playerControl = controls.Player;

        CheckControlType();

        playerControl.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        if(bPhysics)
            playerControl.Jump.performed += _ => pMovementScript.OnJumpPressed();
        else
            playerControl.Jump.performed += _ => movementScript.OnJumpPressed();

        playerControl.LookHorizontal.performed += ctx => lookInput.x = ctx.ReadValue<float>();
        playerControl.LookVertical.performed += ctx => lookInput.y = ctx.ReadValue<float>();

    }

    void CheckControlType()
    {
        if(TryGetComponent<PhysicsMovement>(out PhysicsMovement pMovement))
        {
            pMovementScript = pMovement;
            bPhysics = true;
            bCanControl = true;
            return;
        }

        if(TryGetComponent<Movement>(out Movement cMovement))
        {
            movementScript = cMovement;
            bCanControl = true;
            return;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }

    private void Update()
    {
        if(bCanControl)
        {
            if(bPhysics)
                pMovementScript.Move(movement);
            else
                movementScript.Move(movement);

            playerLook.Look(lookInput);
        }
    }
}
