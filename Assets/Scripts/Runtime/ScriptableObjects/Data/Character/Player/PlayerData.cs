using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.ScriptableObjects.Data.Character.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Character Data/New Player Data")]
    public class PlayerData : CharacterData
    {
        [FormerlySerializedAs("_rollSpeed")] [SerializeField]
        private float rollSpeed;
        public float RollSpeed => rollSpeed;

        [FormerlySerializedAs("_rollCooldown")] [SerializeField]
        private float rollCooldown;
        public float RollCooldown => rollCooldown;

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
