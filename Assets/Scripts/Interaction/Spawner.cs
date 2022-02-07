using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Event
{
    public EnemySpawnerSO spawnSO;
    public override void Activate()
    {
        //Instantiate(spawnObject, transform.position, transform.rotation);
        GameObject entity = Instantiate(spawnSO.EnemyPrefab, transform.position, transform.rotation);
        
        if(entity.TryGetComponent<SimpleEnemy>(out SimpleEnemy data))
        {
            data.Initialize(spawnSO.maxHealth);
            data.UpdateHealth();

            if(spawnSO.Weapon != null)
                data.InstantiateGun(spawnSO.Weapon);
        }
    }
}
