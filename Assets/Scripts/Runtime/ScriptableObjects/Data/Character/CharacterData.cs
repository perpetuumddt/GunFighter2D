using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.ScriptableObjects.Data.Character
{
    public class CharacterData : ScriptableObject
    {
        [FormerlySerializedAs("_name")] [SerializeField]
        private string name;
        public string Name => name;

        [FormerlySerializedAs("_defaultMaxHealth")] [SerializeField]
        private int defaultMaxHealth;
        public int DefaultMaxHealth => defaultMaxHealth;

        [FormerlySerializedAs("_movementSpeed")] [SerializeField]
        private float movementSpeed;
        public float MovementSpeed => movementSpeed;
    
        public event Action<CharacterData> OnDeath;

        public void InvokeOnDeath(CharacterData data)
        {
            OnDeath?.Invoke(data);
        }
    }
}