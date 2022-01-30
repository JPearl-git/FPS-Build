using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Delete");
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
