using Gunfighter.Interface.Detect;
using Gunfighter.Interface.Interact;
using Gunfighter.ScriptableObjects;
using Gunfighter.ScriptableObjects.Data.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Weapon
{
    public class WeaponWorldViewController : MonoBehaviour, IInteractable, IDetectable
    {
        [FormerlySerializedAs("_weaponEvent")] [SerializeField]
        private SpriptableObjectWeaponEvent weaponEvent;

        [FormerlySerializedAs("_weapon")] [SerializeField]
        private Weapon weapon;
        [FormerlySerializedAs("_weaponData")] [SerializeField]
        private WeaponData weaponData;

        public event ObjectDetectedHandler OnObjectDetectedEvent;
        public event ObjectDetectedHandler OnObjectDetectedReleasedEvent;

        public void ActivateInteraction()
        {
            weaponEvent.SetActivePickupWeapon(true, this);
        }

        public void DeactivateInteraction()
        {
            weaponEvent.SetActivePickupWeapon(false, this);
        }

        public void DoInteract()
        {
            Destroy(gameObject);
        }

        public GameObject GameObject { get; }

        public void Detected(GameObject detectionSource)
        {
            ActivateInteraction();
        }

        public void DetectionReleased(GameObject detectionSource)
        {
            DeactivateInteraction();
        }

        public Weapon GetWeapon() 
        {
            return weapon;
        }

        public WeaponData GetWeaponData() 
        {
            return weaponData;
        }
    }
}
