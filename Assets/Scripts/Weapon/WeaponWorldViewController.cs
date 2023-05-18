using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWorldViewController : MonoBehaviour, IInteractable, IDetectable
{
    [SerializeField]
    private SpriptableObjectWeaponEvent _weaponEvent;

    [SerializeField]
    private Weapon _weapon;
    [SerializeField]
    private WeaponData _weaponData;

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

    public Weapon GetWeapon() 
    {
        return _weapon;
    }

    public WeaponData GetWeaponData() 
    {
        return _weaponData;
    }
}
