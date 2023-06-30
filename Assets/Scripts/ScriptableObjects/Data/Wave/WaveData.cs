using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.ScriptableObjects.Data.Wave
{
    [CreateAssetMenu(fileName = "Wave Data", menuName = "Data/Level Data/New Wave Data")]

    public class WaveData : ScriptableObject
    {

        [Serializable]
        public struct Enemies
        {
            [FormerlySerializedAs("_enemyPrefab")] public GameObject enemyPrefab;
            [FormerlySerializedAs("_enemyCount")] public int enemyCount;
            [FormerlySerializedAs("_spawnRate")] public float spawnRate;
        }

        [Serializable]
        public struct Waves
        {
            [FormerlySerializedAs("_counter")] public int counter;
            [FormerlySerializedAs("_enemies")] public Enemies[] enemies;
            [FormerlySerializedAs("_waveDuration")] public float waveDuration;
        }

        [FormerlySerializedAs("_wave")] public Waves[] wave;
    }
}
