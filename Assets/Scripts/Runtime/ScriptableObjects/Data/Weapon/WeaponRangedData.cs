using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.ScriptableObjects.Data.Weapon
{
    [CreateAssetMenu(fileName = "WeaponRangedData", menuName = "Data/Weapon Data/New Weapon Ranged Data")]
    public class WeaponRangedData : WeaponData
    {
        [SerializeField]
        private float reloadTime;
        public float ReloadTime => reloadTime;
    
        [SerializeField]
        private int ammoCountInClip;
        public int AmmoCountInClip => ammoCountInClip;
        
        [SerializeField]
        private int ammoMaxCount;
        public int AmmoMaxCount => ammoMaxCount;
        
        [SerializeField]
        private int spreadAngle;
        public int SpreadAngle => spreadAngle;
        
        [SerializeField] 
        private int bulletsInOneShot;
        public int BulletsInOneShot => bulletsInOneShot;


        [SerializeField] 
        private WeaponRangedAutoShotType autoShotType;
        public WeaponRangedAutoShotType AutoShotType => autoShotType;

    }

    public enum WeaponRangedAutoShotType
    {
        Automatic,
        Manual,
    }
}
