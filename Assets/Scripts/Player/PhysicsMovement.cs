using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] float speed = 25f, jumpForce = 3.5f, slopeForce = 0f;
    [SerializeField] Transform Feet;
    bool jump, isGrounded, isJumping, isDashing;
    float playerHeight;

    Vector3 moveVector, slopeMoveDir;
    RaycastHit slopeHit;
    [SerializeField] ParticleSystem DashParticles;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerHeight = GetComponent<CapsuleCollider>().height * 2;
    }

    void FixedUpdate()
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

    // Solves Collision issues with rb.MovePosition()
    bool HitWall()
    {
        return Physics.Raycast(transform.position, moveVector.normalized * 0.6f, 1f);
    }

    Vector3 GetMoveDirection(Vector2 input)
    {
        moveVector = transform.TransformDirection(input.x,0,input.y) * speed;

        if(HitWall())
            return Vector3.zero;

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
        if(isDashing)
            return;

        rb.MovePosition(transform.position + GetMoveDirection(input) * Time.fixedDeltaTime);

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

    #region Dash Functions
    public void Dash()
    {
        Vector3 dashDirection = moveVector;
        if(moveVector.magnitude == 0)
            dashDirection = transform.forward * speed;

        if(DashParticles != null && !isDashing)
        {
            rb.AddForce(dashDirection * 5, ForceMode.Impulse);
            DashParticles.transform.LookAt(transform.position + DashPos(dashDirection));
            DashParticles.Play();

            rb.drag = 2;
            isDashing = true;
            Invoke("EndDash", .7f);
        }
    }

    private Vector3 DashPos(Vector3 dashDirection)
    {
        float angle = Vector3.SignedAngle(transform.forward, dashDirection, Vector3.up);

        if(Mathf.Abs(angle) > 105f)
            dashDirection = Quaternion.AngleAxis(angle - 180, Vector3.up) * transform.forward;
        else if(angle > 46f)
            dashDirection = Quaternion.AngleAxis(46f, Vector3.up) * transform.forward;
        else if(angle < -46f)
            dashDirection = Quaternion.AngleAxis(-46f, Vector3.up) * transform.forward;

        dashDirection.y = DashParticles.transform.localPosition.y;

        return dashDirection;
    }

    private void EndDash()
    {
        DashParticles.Stop();
        rb.drag = 0;
        isDashing = false;
    }
    #endregion
}