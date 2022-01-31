using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] float speed = 11f, jumpForce = 3.5f, slopeForce = 1000f, stepHeight = 2f;
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
        isGrounded = Physics.Raycast(Feet.position, Vector3.down, 0.3f);
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
        Vector3 checkPoint = Feet.position + (moveVector.normalized * 1f);
        Vector3 oldPoint = checkPoint;
        checkPoint.y += 8f;
        Debug.DrawLine(Feet.position, checkPoint, Color.blue, 0.1f);
        Debug.DrawLine(checkPoint, oldPoint, Color.red, 0.1f);
        
        if(Physics.Raycast(Feet.position, transform.forward, out RaycastHit hitLower, 0.5f))
        {
            Vector3 StepUp = new Vector3(Feet.position.x, Feet.position.y + stepHeight, Feet.position.z);
            if(!Physics.Raycast(Feet.position, moveVector, out RaycastHit hitUpper, 0.6f))
            {
                //
                //Vector3 normal = moveVector.normalized;
                //rb.position += new Vector3(normal.x, stepHeight, normal.z);
                
                
                if(Physics.Raycast(checkPoint, Vector3.down, out RaycastHit hitPos, stepHeight))
                {
                    Debug.Log("Step");
                    Vector3 newPos = hitPos.point;
                    newPos.y += playerHeight / 2;
                    rb.position = newPos;
                }
                else
                    Debug.Log("No Steps");
            }
            //else
            //    Debug.Log("No Object");
        }
        //else
        //    Debug.Log("No Contact");
    }

    public void Move(Vector2 input)
    {
        moveVector = transform.TransformDirection(input.x,0,input.y) * speed;
        slopeMoveDir = Vector3.ProjectOnPlane(moveVector, slopeHit.normal);

        if(!isGrounded)
            moveVector *= 0.5f;

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
        
        
        if(isGrounded && moveVector.magnitude > 0)
            StepClimb();


        if(jump && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
            isJumping = true;
        }
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