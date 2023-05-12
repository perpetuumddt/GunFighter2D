using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWorldViewController : MonoBehaviour, IInteractable
{
    [SerializeField]
    private WeaponData _weaponData;

    public WeaponData WeaponData => _weaponData;

    [SerializeField]
    private SpriptableObjectWeaponEvent _weaponEvent;
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
     //Destroy
    }
}
