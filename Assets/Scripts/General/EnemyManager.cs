using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    EnemySpawner spawner;

    [SerializeField]
    private int _aliveEnemies = 0;
    [SerializeField]
    private int _deadEnemies = 0;

    public event Action<GameObject> OnEnemyDied;

    private void OnEnable()
    {
        spawner.OnSpawn += EnemySpawned;
        OnEnemyDied += EnemyDied;
    }

    private void OnDisable()
    {
        spawner.OnSpawn -= EnemySpawned;
        OnEnemyDied -= EnemyDied;
    }

    private void EnemySpawned(GameObject obj)
    {
        _aliveEnemies+=1;
    }

    private void EnemyDied(GameObject obj)
    {
        _aliveEnemies -= 1;
        _deadEnemies += 1;
    }

    public void InvokeOnEnemyDied(GameObject obj)
    {
        OnEnemyDied?.Invoke(obj);
    }    
}
