using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilControl : MonoBehaviour
{
    Vector3 startPos, startRot;
    Vector3 currentRotation, targetRotation;

    [SerializeField] float recoilX, recoilY, recoilZ;
    [SerializeField] float snapWeight, returnSpeed;

    [SerializeField] PlayerLook playerLook;
    
    void Awake()
    {
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
    }

    public void SetRecoil(Vector3 recoilVector, float snapWeight, float returnSpeed)
    {
        recoilX = recoilVector.x;
        recoilY = recoilVector.y;
        recoilZ = recoilVector.z;

        this.snapWeight = snapWeight;
        this.returnSpeed = returnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snapWeight * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(currentRotation + startRot);

        //Adjusted rotation for playerCam shake
        if(playerLook != null)
        {
            Vector3 recoilRotation = currentRotation;
            recoilRotation.y *= 1.2f;
            playerLook.recoilRotation = currentRotation;
        }
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
