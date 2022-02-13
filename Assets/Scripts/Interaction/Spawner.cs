using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Event
{
    public EnemySpawnerSO spawnSO;
    public override void Activate()
    {
        GameObject entity = Instantiate(spawnSO.EnemyPrefab, transform.position, transform.rotation);

        BotStats data;
        
        if(entity.TryGetComponent<BotStats>(out data))
        {
            InitializeEntity(data);
            return;
        }

        data = entity.GetComponentInChildren<BotStats>();
        if(data != null)
            InitializeEntity(data);
    }

    public void InitializeEntity(BotStats data)
    {
        data.Initialize(spawnSO.maxHealth);
            data.UpdateHealth();

            if(spawnSO.Weapon != null)
                data.InstantiateGun(spawnSO.Weapon);
    }
}
