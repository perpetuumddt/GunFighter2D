using System;
using Gunfighter.Runtime.Entity.Character.Enemy.EnemySpawner;
using Gunfighter.Runtime.ScriptableObjects.Event;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.General
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawner spawner;

        [SerializeField, ReadOnly]
        private int aliveEnemies = 0;
        [SerializeField, ReadOnly]
        private int deadEnemies = 0;

        [SerializeField] private ScriptableObjectExpEvent expChannel;

        public event Action<GameObject> OnEnemyDied;
    

        private void OnEnable()
        {
            spawner.OnSpawn += EnemySpawned;
            OnEnemyDied += EnemyDied;
            OnEnemyDied += expChannel.RaiseEvent;
        }

        private void OnDisable()
        {
            spawner.OnSpawn -= EnemySpawned;
            OnEnemyDied -= EnemyDied;
            OnEnemyDied -= expChannel.RaiseEvent;
        }

        private void EnemySpawned(GameObject obj)
        {
            aliveEnemies+=1;
        }

        private void EnemyDied(GameObject obj)
        {
            aliveEnemies -= 1;
            deadEnemies += 1;
        }

        public void InvokeOnEnemyDied(GameObject obj)
        {
            OnEnemyDied?.Invoke(obj);
        }    
    }
}
