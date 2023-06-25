using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Data", menuName = "Data/Level Data/New Wave Data")]

public class WaveData : ScriptableObject
{
    [SerializeField]
    private int _waveCounter;
    public int WaveCounter => _waveCounter;

    [SerializeField]
    private GameObject[] _enemyPrefabs;
    public GameObject[] EnemyPrefabs => _enemyPrefabs;

    [SerializeField]
    private int[] _enemyCount;
    public int[] EnemyCount => _enemyCount;

    [SerializeField]
    private float _spawnRate;
    public float SpawnRate => _spawnRate;

    [SerializeField]
    private float _waveDuration;
    public float WaveDuration => _waveDuration;

    [SerializeField]
    private float _difficultyModifier;
    public float DifficultyModifier => _difficultyModifier;
}
