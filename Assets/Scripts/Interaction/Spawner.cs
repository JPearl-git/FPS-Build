using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Event
{
    public GameObject spawnObject;
    public override void Activate()
    {
        Instantiate(spawnObject, transform.position, transform.rotation);
    }
}
