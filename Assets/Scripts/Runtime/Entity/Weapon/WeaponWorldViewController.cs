using Gunfighter.Runtime.Interface.Detect;
using Gunfighter.Runtime.Interface.Interact;
using Gunfighter.Runtime.ScriptableObjects;
using Gunfighter.Runtime.ScriptableObjects.Data.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.Entity.Weapon
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
