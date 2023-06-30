using UnityEngine;

namespace ScriptableObjects.Data.Character.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Character Data/New Enemy Data")]
    public class EnemyData : CharacterData
    {
        [SerializeField] private int _damage;
        public int Damage => _damage;

        [SerializeField] private int _baseXpReward;

        public int BaseXpReward => _baseXpReward;
    }
}
