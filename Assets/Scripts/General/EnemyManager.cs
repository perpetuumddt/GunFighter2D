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

    private void OnEnable()
    {
        spawner.OnSpawn += EnemySpawned;
        //characterHealthController.OnHealthZero += EnemyDied;
    }

    private void OnDisable()
    {
        spawner.OnSpawn -= EnemySpawned;
        //characterHealthController.OnHealthZero -= EnemyDied;
    }

    private void EnemySpawned(GameObject obj)
    {
        _aliveEnemies+=1;
    }

    private void EnemyDied()
    {
        _aliveEnemies -= 1;
        _deadEnemies += 1;
    }

}
