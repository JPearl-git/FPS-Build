using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float sensitivityX = 20f;
    [SerializeField] float sensitivityY = 20f;
    float lookX, lookY;

    [SerializeField] Transform playerCam;
    [SerializeField] float xClamp = 85f;
    float xRot = 0f;

    public Vector3 recoilRotation = Vector3.zero;

    //[SerializeField] Animator animator;
    //[SerializeField] Transform DefaultPoint;
    //[SerializeField] Transform MovePoint;
    float transitionSpeed = 5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        SetCamSpot();

        transform.Rotate(Vector3.up, lookX * Time.deltaTime);

        //Temporary, want the head to rotate
        xRot -= lookY * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, -xClamp, xClamp);
        Vector3 targetRot = transform.eulerAngles;
        targetRot.x = xRot;
        playerCam.eulerAngles = targetRot + recoilRotation;
    }

    public void Look(Vector2 input)
    {
        lookX = input.x * sensitivityX * 10;
        lookY = input.y * sensitivityY * 10;
    }

    private void SetCamSpot()
    {
        //if (animator.GetFloat("Speed") > 0)
        //    TransitionCam(MovePoint.position);
        //else TransitionCam(DefaultPoint.position);
    }

    private void TransitionCam(Vector3 endpoint)
    {
        if(playerCam.position != endpoint)
        {
            Vector3 dir = endpoint - playerCam.position;
            float mag = dir.magnitude;
            dir = (dir.normalized * transitionSpeed * Time.deltaTime);
            if (dir.magnitude > mag) playerCam.position = endpoint;
            else playerCam.position += dir;
        }
    }
}
