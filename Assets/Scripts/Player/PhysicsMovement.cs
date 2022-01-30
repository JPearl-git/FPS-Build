using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] float speed = 11f;
    [SerializeField] float jumpForce = 3.5f;
    [SerializeField] Transform Feet;
    bool jump;
    bool isGrounded;

    Vector3 Velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.Raycast(Feet.transform.position, Vector3.down, 0.5f);
    }

    public void Move(Vector2 input)
    {
        Vector3 MoveVector = transform.TransformDirection(input.x,0,input.y) * speed;
        if(!isGrounded)
            MoveVector *= 0.5f;

        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);

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