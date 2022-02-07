using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Enemy Spawner")]
public class EnemySpawnerSO : ScriptableObject
{
    public GameObject EnemyPrefab;

    public GameObject Weapon;

    public int maxHealth = 30;

    void OnValidate()
    {
        if(EnemyPrefab != null)
        {
            if(!EnemyPrefab.GetComponent<SimpleEnemy>())
                EnemyPrefab = null;
        }

        if(Weapon != null)
        {
            if(!Weapon.GetComponent<Gun>())
                Weapon = null;
        }

        if(maxHealth < 1)
            maxHealth = 1;
    }
}
