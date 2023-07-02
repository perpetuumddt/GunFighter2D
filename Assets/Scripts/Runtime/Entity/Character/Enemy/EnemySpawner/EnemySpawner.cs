using System;
using System.Collections;
using System.Collections.Generic;
using Gunfighter.Runtime.General.WaveSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Gunfighter.Runtime.Entity.Character.Enemy.EnemySpawner
{
    public class Enemy : IEquatable<Enemy>
    {
        public int EnemyID { get;  set; }
        public int EnemyCount { get;  set; }
        public float EnemySpawnRate { get; set; }

        public bool Equals(Enemy other)
        {
            if (other == null) return false;
            return (this.EnemyID.Equals(other.EnemyID));
        }

        public override string ToString()
        {
            return "Type: " + EnemyID + ", Count: " + EnemyCount;
        }

        public int GetEnemyID()
        {
            return EnemyID;
        }
    }

    public class EnemySpawner : MonoBehaviour
    {
        [FormerlySerializedAs("_currentWave")]
        [Header("Wave Info")]
        [SerializeField]
        private int currentWave;

        [FormerlySerializedAs("_totalEnemiesLeftToSpawn")] [SerializeField]
        private int totalEnemiesLeftToSpawn;
        [FormerlySerializedAs("_enemySpawned")] [SerializeField]
        private int enemySpawned;

        //private int _enemyAlive;
        //private int _enemyDestroyed;
        //private float _timeUntilNextWave;

        [FormerlySerializedAs("_container")] [SerializeField]
        private Transform container;

        public event Action<GameObject> OnSpawn;

        private bool _isSpawning = true;

        [FormerlySerializedAs("_waveSystemManager")] [SerializeField]
        private WaveSystemManager waveSystemManager;
        //private PoolManager _poolManager; //TODO: integrate pool
        //private UIManager _uiManager; //TODO: integrate ui manager

        private List<Enemy> _enemies;

        private void Start()
        {
            currentWave = waveSystemManager.WaveCounter(0);
            var currentWaveID = currentWave - 1;
            SetupWave(currentWaveID);
        }

        private void SetupWave(int waveID) //TODO: invoke starting wave in game manager
        {
            int amountOfEnemies = waveSystemManager.EnemiesOnTheWaveLenght(waveID);
            int totalAmountOfEnemies = waveSystemManager.GetTotalEnemyAmountToSpawn(waveID);
            enemySpawned = 0;
            totalEnemiesLeftToSpawn = totalAmountOfEnemies;

            SetupEnemyCounter(waveID);

            StartCoroutine(StartWave(waveID, amountOfEnemies, totalAmountOfEnemies));
        }

        private void SetupEnemyCounter(int waveID)
        {
            _enemies = new List<Enemy>();
            for (int i=0; i< waveSystemManager.EnemiesOnTheWaveLenght(waveID);i++)
            {
                _enemies.Add(new Enemy() 
                { 
                    EnemyID = i, 
                    EnemyCount = waveSystemManager.GetEnemyAmountToSpawn(waveID,i), 
                    EnemySpawnRate = waveSystemManager.GetEnemySpawnRate(waveID,i)
                });
            }
        }

        private IEnumerator StartWave(int waveCounter, int amountOfEnemies, int totalAmountOfEnemies) //TODO: spawn random type of enemy in wave
        {
            while (_isSpawning && totalEnemiesLeftToSpawn>0)
            {
                for (int i = 0; i < totalAmountOfEnemies; i++)
                {
                    int randEnemyID = Random.Range(0, amountOfEnemies);
                    if (!CheckIfEnemyTypeIsEmpty(randEnemyID))
                    {
                        SpawnEnemy(waveCounter, randEnemyID);
                        DecrementEnemyAmountLeftToSpawn(randEnemyID);
                        WriteToConsole();
                        yield return new WaitForSeconds(waveSystemManager.GetEnemySpawnRate(waveCounter, randEnemyID));
                    }
                }
            }
        }

        private void WriteToConsole()
        {
            foreach(Enemy enemy in _enemies)
            {
                //Debug.Log(enemy.ToString());
            }
        }

        private void DecrementEnemyAmountLeftToSpawn(int enemyID)
        {
            foreach(Enemy enemy in _enemies) 
            {
                if(enemy.EnemyID == enemyID)
                {
                    enemy.EnemyCount--;
                }
            }
        }

        private bool CheckIfEnemyTypeIsEmpty(int enemyID)
        {
            foreach (Enemy enemy in _enemies)
            {
                if (enemy.EnemyID == enemyID && enemy.EnemyCount == 0)
                {
                    //Debug.Log("Enemy of Type " + enemy.EnemyID + "is Empty");
                    //enemies.Remove(enemy);
                    return true;
                }
            }
            return false;
        }

        private void SpawnEnemy(int waveID, int enemyID)
        {
            int positionIndex = Random.Range(0, 4);

            GameObject enemyToSpawn = waveSystemManager.GetEnemyFromWave(waveID, enemyID);
            Instantiate(enemyToSpawn, CalculateSpawnPosition(positionIndex), Quaternion.identity, container);
            InvokeOnSpawn(enemyToSpawn);
            totalEnemiesLeftToSpawn -= 1;
            enemySpawned += 1;
        }

        public void InvokeOnSpawn(GameObject obj)
        {
            OnSpawn?.Invoke(obj);
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
}