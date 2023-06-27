using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveData;

public class WaveSystemManager : MonoBehaviour
{
    [SerializeField]
    private WaveData _waveData;

    public int WavesCountLenght()
    {
        return _waveData._wave.Length;
    }

    public float WaveDuration(int waveID)
    {
        return _waveData._wave[waveID]._waveDuration;
    }

    public int WaveCounter(int waveID)
    {
        return _waveData._wave[waveID]._counter;
    }

    public int EnemiesOnTheWaveLenght(int waveID)
    {
        return _waveData._wave[waveID]._enemies.Length;
    }

    public GameObject GetEnemyFromWave(int waveID, int enemyID)
    {
        return _waveData._wave[waveID]._enemies[enemyID]._enemyPrefab;
    }

    public int GetEnemyAmountToSpawn(int waveID, int enemyID)
    {
        return _waveData._wave[waveID]._enemies[enemyID]._enemyCount;
    }

    public int GetTotalEnemyAmountToSpawn(int waveID)
    {
        int totalAmount = 0;
        for (int i = 0; i < EnemiesOnTheWaveLenght(waveID); i++)
        {
            totalAmount += GetEnemyAmountToSpawn(waveID, i);
        }

        return totalAmount;
    }

    public float GetEnemySpawnRate(int waveID, int enemyID)
    {
        return _waveData._wave[waveID]._enemies[enemyID]._spawnRate;
    }
}
