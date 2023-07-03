using System;
using Gunfighter.Runtime.Entity.Weapon;
using UnityEngine;

namespace Gunfighter.Runtime.ScriptableObjects.Event
{
    [CreateAssetMenu(fileName = "Weapon Event", menuName = "Data/Event/New Weapon Event")]
    public class SpriptableObjectWeaponEvent : ScriptableObject
    {
        public event Action<bool, WeaponWorldViewController> OnSetActivePickupWeapon;

        public void SetActivePickupWeapon(bool isActive, WeaponWorldViewController weaponWorldViewController)
        {
            OnSetActivePickupWeapon?.Invoke(isActive, weaponWorldViewController);
        }
    }
}
