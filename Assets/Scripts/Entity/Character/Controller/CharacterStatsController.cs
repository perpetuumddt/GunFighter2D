using ScriptableObjects.Data.Character;
using UnityEngine;

namespace Entity.Character.Controller
{
    public class CharacterStatsController : MonoBehaviour
    {
        [SerializeField]
        private CharacterData _characterData;
        public CharacterData CharacterData => _characterData;

        public float Health => _characterData.DefaultMaxHealth;
        public float MovementSpeed => _characterData.MovementSpeed;


        public virtual void UpdateStats()
        {
        
        }
    }
}
