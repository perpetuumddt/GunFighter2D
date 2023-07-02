using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.ScriptableObjects.Data.Wave
{
    [CreateAssetMenu(fileName = "Wave Data", menuName = "Data/Level Data/New Wave Data")]

    public class WaveData : ScriptableObject
    {

        [Serializable]
        public struct Enemies
        {
            public GameObject enemyPrefab;
            public int enemyCount;
            public float spawnRate;
        }

        [Serializable]
        public struct Waves
        {
            public int counter;
            public Enemies[] enemies;
            public float waveDuration;
        }

        [FormerlySerializedAs("_wave")] public Waves[] wave;
    }
}
