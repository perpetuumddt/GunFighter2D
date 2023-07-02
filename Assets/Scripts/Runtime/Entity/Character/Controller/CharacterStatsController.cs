using Gunfighter.Runtime.ScriptableObjects.Data.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.Entity.Character.Controller
{
    public class CharacterStatsController : MonoBehaviour
    {
        [FormerlySerializedAs("_characterData")] [SerializeField]
        private CharacterData characterData;
        public CharacterData CharacterData => characterData;

        public float Health => characterData.DefaultMaxHealth;
        public float MovementSpeed => characterData.MovementSpeed;


        public virtual void UpdateStats()
        {
        
        }
    }
}
