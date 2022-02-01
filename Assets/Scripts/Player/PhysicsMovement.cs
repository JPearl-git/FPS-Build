using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] float speed = 11f, jumpForce = 3.5f, slopeForce = 1000f, stepHeight = 0.4f;
    [SerializeField] Transform Feet;
    bool jump, isGrounded, isJumping;
    float playerHeight;

    Vector3 moveVector, slopeMoveDir;
    RaycastHit slopeHit;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerHeight = GetComponent<CapsuleCollider>().height * 2;
    }

    void Update()
    {
        isGrounded = Physics.Raycast(Feet.position + Vector3.up, Vector3.down, 1.3f);
        if(isGrounded && isJumping && rb.velocity.y < 0)
            isJumping = false;
    }

    // Is the RigidBody on a slope
    bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
                return true;
        }

        return false;
    }

    //WIP step up stairs still not working
    void StepClimb()
    {
        if(Physics.Raycast(Feet.position, moveVector, out RaycastHit hitLower, 0.5f))
        {
            Vector3 StepUp = new Vector3(Feet.position.x, Feet.position.y + stepHeight, Feet.position.z);
            //Debug.DrawLine(StepUp, StepUp + (moveVector * 0.6f), Color.red, 0.1f);
            if(!Physics.Raycast(StepUp, moveVector, out RaycastHit hitUpper, 0.6f))
            {
                Vector3 checkPoint = StepUp + (moveVector.normalized * 0.65f);
                if(Physics.Raycast(checkPoint, Vector3.down, out RaycastHit hitPos, stepHeight))
                {
                    if(hitPos.normal == Vector3.up)
                    {
                        Vector3 newPos = hitPos.point;
                        newPos.y += (transform.position.y - Feet.position.y);
                        rb.position = newPos;
                    }
                }
                //else
                //    Debug.DrawLine(checkPoint, checkPoint - (Vector3.down * stepHeight), Color.green, 3f);
            }
        }
    }

    public void Move(Vector2 input)
    {
        moveVector = transform.TransformDirection(input.x,0,input.y) * speed;
        slopeMoveDir = Vector3.ProjectOnPlane(moveVector, slopeHit.normal);

        if(!isGrounded)
            moveVector *= 0.5f;

        // Go up Slope
        if(isGrounded && OnSlope())
        {
            rb.velocity = new Vector3(slopeMoveDir.x, 0, slopeMoveDir.z);
            if(Feet.position.y > slopeHit.point.y && slopeMoveDir.y < 0)
                rb.AddForce(Vector3.down * slopeForce, ForceMode.VelocityChange);
        }
        else
        {
            Vector3 velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
            rb.velocity = velocity;
        }   
        
        // Climp Stairs
        if(isGrounded && moveVector.magnitude > 0)
            StepClimb();

        // Jump
        if(jump && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
            isJumping = true;
        }
        // Go down Slope or stairs
        else if(!isJumping && rb.velocity.y > 0)
        {
            Vector3 velocity = rb.velocity;
            velocity.y = 0;
            rb.velocity = velocity;
        }
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
}