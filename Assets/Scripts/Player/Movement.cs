using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    Vector2 movement;

    [SerializeField] float jumpHeight = 3.5f;
    bool jump;
    [SerializeField] float gravity = -30f;
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float lastVertPos = 0;

    //[SerializeField] Animator animator;

    Vector3 Velocity;

    private void Update()
    {
        //Vector3 center = controller.center;
        //center.y = ControllerAnchor.position.y;
        //controller.center = center;

        // Check if player is grounded
        isGrounded = 0f == (transform.position.y - lastVertPos) / Time.deltaTime;
        lastVertPos = transform.position.y;
        if (isGrounded) verticalVelocity.y = 0;

        // Set Velocity based on movement inputs
        Velocity = (transform.right * movement.x + transform.forward * movement.y) * speed;
        controller.Move(Velocity * Time.deltaTime);

        // Jump if player is allowed to
        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            jump = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

        // Change Animation based on move state
        CheckMoveState();
    }

    public void Move(Vector2 input)
    {
        movement = input;
        //print(movement);
    }

    public void OnJumpPressed()
    {
        jump = true;
    }

    private void CheckMoveState()
    {
        //animator.SetFloat("Speed", Velocity.magnitude);
        //animator.SetBool("Idling", Velocity.magnitude == 0 && isGrounded);

        //if(animator.GetBool("Idling"))
    }
}