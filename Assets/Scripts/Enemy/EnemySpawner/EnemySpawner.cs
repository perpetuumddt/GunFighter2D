using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour //TODO: Integrate pool object
{
    [SerializeField]
    private WaveData[] _waveData;

    private bool _canSpawn = true;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (_canSpawn)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_waveData[0].SpawnRate);
        }
        yield return new WaitForSeconds(_waveData[0].WaveDuration);
    }

    private void SpawnEnemy()
    {
        int rand = Random.Range(0, _waveData[0].EnemyPrefabs.Length);
        int positionIndex = Random.Range(0, 4);
        GameObject enemyToSpawn = _waveData[0].EnemyPrefabs[rand];

        Instantiate(enemyToSpawn, CalculateSpawnPosition(positionIndex), Quaternion.identity);
    }

    private Vector3 CalculateSpawnPosition(int positionIndex)
    {
        float randPos = (float)Random.Range(0,101) / 100;
        switch (positionIndex) //0-Top 1-Right 2-Down 3-Left
        {
            case 0:
                return Camera.main.ViewportToWorldPoint(new Vector3(randPos, 1.1f, 0f));
            case 1:
                return Camera.main.ViewportToWorldPoint(new Vector3(1.1f, randPos, 0f));
            case 2:
                return Camera.main.ViewportToWorldPoint(new Vector3(randPos, -0.1f, 0f));
            case 3:
                return Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, randPos, 0f));
        }
        return new Vector3(1.1f,1.1f,0);
    }
}
