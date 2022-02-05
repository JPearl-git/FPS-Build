using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningEffect : MonoBehaviour
{
    public Transform target;

    int damage;

    public void Initialize(float duration = 3f, int damage = 2)
    {
        this.damage = damage;
        StartCoroutine("Tick");
        Destroy(gameObject, duration);
    }
    
    void Update()
    {
        transform.position = target.position + Vector3.down;
    }

    void Effect()
    {
        
    }

    IEnumerator Tick()
    {
        while(true)
        {
            Effect();
            yield return new WaitForSeconds(1);
        }
    }
}
