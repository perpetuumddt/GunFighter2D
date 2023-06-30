using System;
using UnityEngine;

namespace ScriptableObjects.Data.Wave
{
    [CreateAssetMenu(fileName = "Wave Data", menuName = "Data/Level Data/New Wave Data")]

    public class WaveData : ScriptableObject
    {

        [Serializable]
        public struct Enemies
        {
            public GameObject _enemyPrefab;
            public int _enemyCount;
            public float _spawnRate;
        }

        [Serializable]
        public struct Waves
        {
            public int _counter;
            public Enemies[] _enemies;
            public float _waveDuration;
        }

        public Waves[] _wave;
    }
}
