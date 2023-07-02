using Gunfighter.Runtime.ScriptableObjects.Data.Wave;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.General.WaveSystem
{
    public class WaveSystemManager : MonoBehaviour
    {
        [FormerlySerializedAs("_waveData")] [SerializeField]
        private WaveData waveData;

        public int WavesCountLenght()
        {
            return waveData.wave.Length;
        }

        public float WaveDuration(int waveID)
        {
            return waveData.wave[waveID].waveDuration;
        }

        public int WaveCounter(int waveID)
        {
            return waveData.wave[waveID].counter;
        }

        public int EnemiesOnTheWaveLenght(int waveID)
        {
            return waveData.wave[waveID].enemies.Length;
        }

        public GameObject GetEnemyFromWave(int waveID, int enemyID)
        {
            return waveData.wave[waveID].enemies[enemyID].enemyPrefab;
        }

        public int GetEnemyAmountToSpawn(int waveID, int enemyID)
        {
            return waveData.wave[waveID].enemies[enemyID].enemyCount;
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
            return waveData.wave[waveID].enemies[enemyID].spawnRate;
        }
    }
}
