using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Scripts
    HUD_UI ui;
    Movement movementScript;
    PhysicsMovement pMovementScript;
    [SerializeField] PlayerLook playerLook;
    #endregion

    #region Controller
    InputControls controls;
    InputControls.PlayerActions playerControl;
    #endregion

    #region Input Vectors
    Vector2 movement;
    Vector2 lookInput;
    #endregion

    [HideInInspector] public bool bCanControl;
    bool bPhysics;

    IButtonInteract interact;

    #region  Unity Basic Functions
    void Awake()
    {
        GameObject.Find("HUD").TryGetComponent<HUD_UI>(out ui);

        controls = new InputControls();
        playerControl = controls.Player;

        CheckControlType();

        playerControl.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        if(bPhysics)
        {
            playerControl.Jump.performed += _ => pMovementScript.OnJumpPressed();
            playerControl.Dash.performed += _ => pMovementScript.Dash();
        }
        else
            playerControl.Jump.performed += _ => movementScript.OnJumpPressed();

        playerControl.LookHorizontal.performed += ctx => lookInput.x = ctx.ReadValue<float>();
        playerControl.LookVertical.performed += ctx => lookInput.y = ctx.ReadValue<float>();

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
            if(!bPhysics)
                movementScript.Move(movement);

            playerLook.Look(lookInput);
        }
    }

    private void FixedUpdate()
    {
        if(bPhysics)
            pMovementScript.Move(movement);
    }
    #endregion

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

    #region Interaction Functions
    public void SetInteract(IButtonInteract buttonInteract)
    {
        if(interact == buttonInteract)
            return;

        if(interact == null)
        {
            interact = buttonInteract;
        }
        else
        {
            float currentDistance = (interact.transform.position - transform.position).magnitude;
            float newDistance = (buttonInteract.transform.position - transform.position).magnitude;
            if(newDistance < currentDistance)
                interact = buttonInteract;
        }

        if(ui != null)
            ui.ShowInteractPrompt();
    }

    public void RemoveInteractive(IButtonInteract buttonInteract)
    {
        if(interact == buttonInteract)
        {
            interact = null;

            if(ui != null)
                ui.HideInteractPrompt();
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        //Debug.Log("Press Interact");
        if(!context.performed || interact == null)
            return;

        interact.Interact();
    }
    #endregion
}
