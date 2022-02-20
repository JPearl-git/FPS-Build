using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : ISwitchable
{
    Transform pivot;

    new void Start()
    {
        pivot = transform.GetChild(0);
        base.Start();
    }

    public override void Activate()
    {
            StopAllCoroutines();
            StartCoroutine(MoveDoor(110));
    }

    public override void Deactivate()
    {
            StopAllCoroutines();
            StartCoroutine(MoveDoor(0));
    }

    IEnumerator MoveDoor(float rot)
    {
        var fromAngle = pivot.localRotation;
        var toAngle = Quaternion.Euler(0,rot,0);
        for(var t = 0f; t < 1; t += Time.deltaTime/1.2f) {
            pivot.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }
}
