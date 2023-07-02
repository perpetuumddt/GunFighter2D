using Gunfighter.Runtime.ScriptableObjects.Data.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.Entity.Weapon
{
    public class WeaponEffectData : WeaponData
    {
        [FormerlySerializedAs("_effect")] [SerializeField]
        private string effect;

        [FormerlySerializedAs("_effectDuration")] [SerializeField]
        private float effectDuration;
    }
}
