using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] float speed = 11f, jumpForce = 3.5f, slopeForce = 1000f;
    [SerializeField] Transform Feet;
    bool jump, isGrounded;
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
        isGrounded = Physics.Raycast(Feet.position, Vector3.down, 0.5f);
    }

    bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                Debug.Log("On Slope");
                return true;
            }
        }
        Debug.Log("Not on Slope");

        return false;
    }

    public void Move(Vector2 input)
    {
        moveVector = transform.TransformDirection(input.x,0,input.y) * speed;
        slopeMoveDir = Vector3.ProjectOnPlane(moveVector, slopeHit.normal);

        if(!isGrounded)
            moveVector *= 0.5f;

        if(isGrounded && OnSlope())
        {
            //rb.AddForce(new Vector3(slopeMoveDir.x, rb.velocity.y, slopeMoveDir.z), ForceMode.Acceleration);
            rb.velocity = new Vector3(slopeMoveDir.x, 0, slopeMoveDir.z);
            if(Feet.position.y > slopeHit.point.y && slopeMoveDir.y < 0)
                rb.AddForce(Vector3.down * slopeForce, ForceMode.VelocityChange);
            Debug.DrawLine(Feet.position, slopeHit.point, Color.magenta, 0.1f);
            Debug.DrawLine(transform.position, transform.position + slopeMoveDir, Color.green, 0.3f);
            Debug.DrawLine(transform.position, transform.position + (Vector3.down * slopeForce), Color.red, 0.3f);
        }
        else
        {
            //rb.AddForce(new Vector3(moveVector.x, rb.velocity.y, moveVector.z), ForceMode.Acceleration);
            rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
            Debug.DrawLine(transform.position, transform.position + moveVector, Color.green, 0.3f);
        }

        if(jump && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
}