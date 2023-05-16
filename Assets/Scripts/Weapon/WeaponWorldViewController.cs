using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWorldViewController : Weapon, IInteractable,IDetectable
{
    [SerializeField]
    private SpriptableObjectWeaponEvent _weaponEvent;

    public event ObjectDetectedHandler OnObjectDetectedEvent;
    public event ObjectDetectedHandler OnObjectDetectedReleasedEvent;

    public void ActivateInteraction()
    {
        _weaponEvent.SetActivePickupWeapon(true, this);
    }

    public void DeactivateInteraction()
    {
        _weaponEvent.SetActivePickupWeapon(false, this);
    }

    public void DoInteract()
    {
        Destroy(gameObject);
    }

    public void Detected(GameObject detectionSource)
    {
        ActivateInteraction();
    }

    public void DetectionReleased(GameObject detectionSource)
    {
        DeactivateInteraction();
    }
}
