using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movementScript;
    [SerializeField] PlayerLook playerLook;

    InputControls controls;
    InputControls.PlayerActions playerControl;

    Vector2 movement;
    Vector2 lookInput;

    [HideInInspector] public bool bCanControl;

    void Awake()
    {
        controls = new InputControls();
        playerControl = controls.Player;
        bCanControl = true;

        playerControl.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
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
            movementScript.Move(movement);
            playerLook.Look(lookInput);
        }
    }
}
