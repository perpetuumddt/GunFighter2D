using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour //TODO: Integrate pool object
{
    [SerializeField]
    private float _spawnRate = 2f;

    [SerializeField]
    private GameObject[] _enemyPrefabs;

    [SerializeField]
    private bool _canSpawn;


    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while(_canSpawn)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnRate);
        }    
    }

    private void SpawnEnemy()
    {
        int rand = Random.Range(0, _enemyPrefabs.Length);
        GameObject enemyToSpawn = _enemyPrefabs[rand];
        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
    }
}
