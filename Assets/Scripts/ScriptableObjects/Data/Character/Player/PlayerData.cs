using UnityEngine;

namespace ScriptableObjects.Data.Character.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Character Data/New Player Data")]
    public class PlayerData : CharacterData
    {
        [SerializeField]
        private float _rollSpeed;
        public float RollSpeed => _rollSpeed;

        [SerializeField]
        private float _rollCooldown;
        public float RollCooldown => _rollCooldown;

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
