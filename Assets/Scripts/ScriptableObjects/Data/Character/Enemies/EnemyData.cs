using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.ScriptableObjects.Data.Character.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Character Data/New Enemy Data")]
    public class EnemyData : CharacterData
    {
        [FormerlySerializedAs("_damage")] [SerializeField] private int damage;
        public int Damage => damage;

        [FormerlySerializedAs("_baseXpReward")] [SerializeField] private int baseXpReward;

        public int BaseXpReward => baseXpReward;
    }
}
