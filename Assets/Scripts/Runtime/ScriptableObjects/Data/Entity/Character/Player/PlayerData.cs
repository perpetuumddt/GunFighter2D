using UnityEngine;

namespace Gunfighter.Runtime.ScriptableObjects.Data.Entity.Character.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Character Data/New Player Data")]
    public class PlayerData : CharacterData
    {
        [SerializeField]
        private float rollSpeed;
        public float RollSpeed => rollSpeed;
        
        [SerializeField]
        private float rollCooldown;
        public float RollCooldown => rollCooldown;

        [SerializeField] private float rollDuration;
        public float RollDuration => rollDuration;
        
        
        [SerializeField] private AnimationCurve experienceLevelDistribution;

        public AnimationCurve ExperienceLevelDistribution
        {
            get => experienceLevelDistribution;
            private set
            {
                experienceLevelDistribution = value;
            }
        }

        public static PlayerData CreateInstance(AnimationCurve experienceLevelDisptirbution)
        {
            var data = ScriptableObject.CreateInstance<PlayerData>();
            data.ExperienceLevelDistribution = experienceLevelDisptirbution;
            return data;
        }
    

    }
}
