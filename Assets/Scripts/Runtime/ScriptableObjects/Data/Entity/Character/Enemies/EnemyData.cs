using UnityEngine;

namespace Gunfighter.Runtime.ScriptableObjects.Data.Entity.Character.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Character Data/New Enemy Data")]
    public class EnemyData : CharacterData
    {
        [SerializeField] private int damage;
        public int Damage => damage;

        [SerializeField] private int baseXpReward;
        public int BaseXpReward => baseXpReward;
    }
}
