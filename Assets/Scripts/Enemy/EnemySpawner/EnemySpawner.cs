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
        while (_canSpawn)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    private void SpawnEnemy()
    {
        int rand = Random.Range(0, _enemyPrefabs.Length);
        int positionIndex = Random.Range(0, 4);
        Debug.Log(positionIndex);
        GameObject enemyToSpawn = _enemyPrefabs[rand];

        Instantiate(enemyToSpawn, CalculateSpawnPosition(positionIndex), Quaternion.identity);
    }

    private Vector3 CalculateSpawnPosition(int positionIndex)
    {
        float randPos = (float)Random.Range(0,101) / 100;
        Debug.Log(randPos);
        switch (positionIndex) //0-Top 1-Right 2-Down 3-Left
        {
            case 0:
                return Camera.main.ViewportToWorldPoint(new Vector3(randPos, 1.1f, 0f));
                break;
            case 1:
                return Camera.main.ViewportToWorldPoint(new Vector3(1.1f, randPos, 0f));
                break;
            case 2:
                return Camera.main.ViewportToWorldPoint(new Vector3(randPos, -0.1f, 0f));
                break;
            case 3:
                return Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, randPos, 0f));
                break;

        }
        return new Vector3(1.1f,1.1f,0);
    }
}
