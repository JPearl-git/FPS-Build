using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMovement : MonoBehaviour
{
    Rigidbody rb;
    HUD_UI ui;
    Vector2 movement;

    public float speed = 25f, slopeForce = 0f;
    float playerHeight;
    [SerializeField] Transform Feet;
    bool isGrounded;

    #region Jump Variables
    [Header("Jump Variables")]
    public float jumpForce = 3.5f;

    bool jump, isJumping;
    #endregion;

    #region Dash Variables
    [Header("Dash Variables")]
    public float dashForce = 5f;
    public float dashDelay = 2f;
    float dashPercent = 1f;
    
    bool isDashing, canDash, canChargeDash;
    #endregion

    Vector3 moveVector, slopeMoveDir;
    RaycastHit slopeHit;
    [SerializeField] ParticleSystem DashParticles;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ui = GameObject.Find("HUD").GetComponent<HUD_UI>();
        playerHeight = GetComponent<CapsuleCollider>().height * 2;
    }

    void FixedUpdate()
    {
        // Check if Player is grounded
        isGrounded = Physics.Raycast(Feet.position + Vector3.up, Vector3.down, 1.3f);
        if(isGrounded && isJumping && rb.velocity.y < 0)
            isJumping = false;

        // If grounded check player's slope
        if(isGrounded)
        {
            RaycastHit floor = GetFloor();
            if(floor.transform != null && floor.normal != Vector3.up)
            {
                rb.AddForce(Vector3.down * slopeForce, ForceMode.Force);
                Debug.Log("Floor detected " + floor.normal);
            }
        }

        // Check if player can dash
        if(!canDash)
        {
            if(dashPercent == 1f)
                canDash = true;
            else if(canChargeDash)
            {
                dashPercent += Time.fixedDeltaTime;
                if(dashPercent > 1)
                    dashPercent = 1;
                if(ui != null)
                    ui.SetSprint(dashPercent);
            }
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
        {
            if(rb.SweepTest(moveVector, out RaycastHit hit, rb.velocity.magnitude * Time.fixedDeltaTime))
            {
                if(hit.point.y > Feet.position.y)
                    return new Vector3(0, moveVector.y, 0);
            }

            return moveVector * 0.5f;
        }

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
        if(!canDash)
            return;
        
        Vector3 dashDirection = moveVector;
        if(moveVector.magnitude == 0)
            dashDirection = transform.forward * speed;

        if(DashParticles == null || isDashing)
            return;

        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
        DashParticles.transform.LookAt(transform.position + DashPos(dashDirection));
        DashParticles.Play();

        dashPercent = 0f;
        isDashing = true;
        canDash = false;
        canChargeDash = false;

        if(ui != null)
            ui.SetSprint(0);

        Invoke("EndDash", .5f);
    }

    Vector3 DashPos(Vector3 dashDirection)
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

    void EndDash()
    {
        DashParticles.Stop();

        isDashing = false;
        rb.velocity = Vector3.zero;

        Invoke("ChargeDash", dashDelay);
    }

    void ChargeDash()
    {
        canChargeDash = true;
    }
    #endregion
}