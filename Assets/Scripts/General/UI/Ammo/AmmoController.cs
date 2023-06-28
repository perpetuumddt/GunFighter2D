using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;

public class AmmoController : MonoBehaviour
{
    private GameObject _ammoUnitPrefab;

    private GameObject _containerElement;

   private GameObject[] _ammoUnits;

    public AmmoController(GameObject ammoUnitPrefab, GameObject containerElement)
    {
        _ammoUnitPrefab = ammoUnitPrefab;
        _containerElement = containerElement;
    }

    
    public void UpdateAmmoController(int value)
    {
        for(int i = _ammoUnits.Length-1; i >= 0; i--) 
        {
            if(i>=value)
            {
                _ammoUnits[i].GetComponent<Image>().color = Color.gray;
            }
            else
            {
                _ammoUnits[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
    
    public void SetupAmmoController(int value, GameObject UIObject)  
    {
        if (value < 0) { throw new ArgumentOutOfRangeException(); }

        if(_ammoUnits!=null && _ammoUnits.Length>0)
        {
            foreach (GameObject ammo in _ammoUnits)
            {
                Destroy(ammo);
            }
        }

        _ammoUnits = new GameObject[value];
        
        for (int i = 0; i < value; i++)
        {
            _ammoUnits[i] = Instantiate(_ammoUnitPrefab, UIObject.transform);

            Vector3 location = new Vector3(-2.5f, 2f + i * 3, 0);
            _ammoUnits[i].transform.SetLocalPositionAndRotation(location, new Quaternion());
            Vector3 containerLocation = new Vector3(location.x - 40.5f, location.y - 10f, 0);
            _containerElement.transform.SetLocalPositionAndRotation(containerLocation, new Quaternion(0, 0, 180, 0));
        }
    }

}
