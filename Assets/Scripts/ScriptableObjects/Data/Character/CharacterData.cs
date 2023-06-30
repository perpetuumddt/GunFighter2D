using System;
using UnityEngine;

namespace ScriptableObjects.Data.Character
{
    public class CharacterData : ScriptableObject
    {
        [SerializeField]
        private string _name;
        public string Name => _name;

        [SerializeField]
        private int _defaultMaxHealth;
        public int DefaultMaxHealth => _defaultMaxHealth;

        [SerializeField]
        private float _movementSpeed;
        public float MovementSpeed => _movementSpeed;
    
        public event Action<CharacterData> OnDeath;

        public void InvokeOnDeath(CharacterData data)
        {
            OnDeath?.Invoke(data);
        }
    }
}