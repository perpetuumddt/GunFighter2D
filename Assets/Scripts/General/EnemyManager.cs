using System;
using Gunfighter.Entity.Character.Enemy.EnemySpawner;
using Gunfighter.ScriptableObjects.Event;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.General
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        EnemySpawner spawner;

        [FormerlySerializedAs("_aliveEnemies")] [SerializeField]
        private int aliveEnemies = 0;
        [FormerlySerializedAs("_deadEnemies")] [SerializeField]
        private int deadEnemies = 0;

        [FormerlySerializedAs("_expChannel")] [SerializeField] private ScriptableObjectExpEvent expChannel;

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
