using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilControl : MonoBehaviour
{
    Vector3 startPos, currentRotation, targetRotation;

    [SerializeField] float recoilX, recoilY, recoilZ;
    [SerializeField] float snapWeight, returnSpeed;
    
    void Awake()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snapWeight * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.RandomRange(-recoilY, recoilY), Random.RandomRange(-recoilZ, recoilZ));
    }
}
