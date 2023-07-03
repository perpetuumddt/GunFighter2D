using System;
using Gunfighter.Runtime.ScriptableObjects.Generic;
using UnityEngine;

namespace Gunfighter.Runtime.ScriptableObjects.Data.Entity
{
    public class EntityData : DescriptionBaseSO
    {
        [Space]
        [Header("Entity Data")]
        [SerializeField]
        private string name;
        public string Name => name;
        [Space]
        [SerializeField]
        private int defaultMaxHealth;
        public int DefaultMaxHealth => defaultMaxHealth;

        [SerializeField] private bool isIndestructible;
        public bool IsIndestructible => isIndestructible;

        [SerializeField] private int collisionDamage;
        public int CollisionDamage => collisionDamage;
        
        
        public event Action<EntityData> OnDeath;
        public void InvokeOnDeath(EntityData data)
        {
            OnDeath?.Invoke(data);
        }
    }
}
