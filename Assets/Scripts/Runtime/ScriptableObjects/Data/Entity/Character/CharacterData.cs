using UnityEngine;

namespace Gunfighter.Runtime.ScriptableObjects.Data.Entity.Character
{
    public class CharacterData : EntityData
    {
        [Header("Character Data")]
        [SerializeField]
        private float movementSpeed;
        public float MovementSpeed => movementSpeed;
    

        
    }
}