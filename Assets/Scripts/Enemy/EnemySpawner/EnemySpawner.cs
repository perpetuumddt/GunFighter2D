using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    [Header("Wave Info")]
    [SerializeField]
    private int _currentWave;

    [SerializeField]
    private int _enemyLeftToSpawn;
    [SerializeField]
    private int _enemySpawned;

    private int _enemyAlive;
    private int _enemyDestroyed;

    private float _timeUntilNextWave;

    private bool _isSpawning = true;

    [SerializeField]
    private WaveSystemManager _waveSystemManager;
    //private PoolManager _poolManager; //TODO: integrate pool
    //private UIManager _uiManager; //TODO: integrate ui manager

    private void Start()
    {
        _currentWave = _waveSystemManager.WaveCounter(0);
        var _currentWaveID = _currentWave - 1;
        SetupWave(_currentWaveID);
    }

    private void SetupWave(int waveID) //TODO: invoke starting wave in game manager
    {
        int amountOfEnemies = _waveSystemManager.EnemiesOnTheWaveLenght(waveID);
        int totalAmountOfEnemies = _waveSystemManager.GetTotalEnemyAmountToSpawn(waveID);
        _enemySpawned = 0;
        _enemyLeftToSpawn = totalAmountOfEnemies;
        StartCoroutine(StartWave(waveID, amountOfEnemies, totalAmountOfEnemies));
    }

    private IEnumerator StartWave(int waveCounter, int amountOfEnemies, int totalAmountOfEnemies) //TODO: spawn random type of enemy in wave
    {
        while (_isSpawning && _enemyLeftToSpawn>0)
        {
            for (int i = 0; i < totalAmountOfEnemies; i++)
            {
                int randEnemyID = Random.Range(0, _waveSystemManager.EnemiesOnTheWaveLenght(waveCounter));
                SpawnEnemy(waveCounter, randEnemyID);
                yield return new WaitForSeconds(_waveSystemManager.GetEnemySpawnRate(waveCounter, randEnemyID));
                _enemyLeftToSpawn -= 1;
                _enemySpawned += 1;
            }
        }
    }

    private void SpawnEnemy(int waveID, int enemyID)
    {
        int positionIndex = Random.Range(0, 4);

        GameObject enemyToSpawn = _waveSystemManager.GetEnemyFromWave(waveID, enemyID);
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
