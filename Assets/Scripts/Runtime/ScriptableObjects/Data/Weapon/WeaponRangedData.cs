using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.ScriptableObjects.Data.Weapon
{
    [CreateAssetMenu(fileName = "WeaponRangedData", menuName = "Data/Weapon Data/New Weapon Ranged Data")]
    public class WeaponRangedData : WeaponData
    {
        [FormerlySerializedAs("_reloadTime")] [SerializeField]
        private float reloadTime;
        public float ReloadTime => reloadTime;
    
        [FormerlySerializedAs("_ammoCountInClip")] [SerializeField]
        private int ammoCountInClip;
        public int AmmoCountInClip => ammoCountInClip;


        [FormerlySerializedAs("_ammoMaxCount")] [SerializeField]
        private int ammoMaxCount;
        public int AmmoMaxCount => ammoMaxCount;


        [FormerlySerializedAs("_spreadAngle")] [SerializeField]
        private int spreadAngle;
        public int SpreadAngle => spreadAngle;


        [FormerlySerializedAs("_bulletsInOneShot")] [SerializeField] 
        private int bulletsInOneShot;
        public int BulletsInOneShot => bulletsInOneShot;

    }
}
