using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] float speed = 25f, jumpForce = 3.5f, slopeForce = 0f, stepHeight = 0.4f;
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

        if(isGrounded)
        {
            RaycastHit floor = GetFloor();
            if(floor.transform != null && floor.normal != Vector3.up)
                rb.AddForce(Vector3.down * slopeForce, ForceMode.Force);

        }
    }

    RaycastHit GetFloor()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out RaycastHit hit);

        return hit;
    }

    Vector3 GetMoveDirection(Vector2 input)
    {
        moveVector = transform.TransformDirection(input.x,0,input.y) * speed;

        if(!isGrounded)
            return moveVector *= 0.5f;

        var floor = GetFloor();
        if(floor.transform == null || floor.normal.Equals(Vector3.up))
            return moveVector;

        var slopeVector = new Vector3(moveVector.x, -floor.normal.y * 2, moveVector.z);
        return slopeVector;

        

    }

    public void Move(Vector2 input)
    {
        rb.MovePosition(transform.position + GetMoveDirection(input) * Time.deltaTime);

        // Jump
        if(jump && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
            isJumping = true;
        }
    }

    public void ForcedLaunch()
    {
        isJumping = true;
    }

    public bool CheckGrounded()
    {
        return isGrounded;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }
}