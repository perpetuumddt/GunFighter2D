using ScriptableObjects.Data.Weapon;
using UnityEngine;

namespace Entity.Weapon
{
    public class WeaponEffectData : WeaponData
    {
        [SerializeField]
        private string _effect;

        [SerializeField]
        private float _effectDuration;
    }
}
