using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController
{
    private GameObject _heartPrefab;
    
    
    private Stack<GameObject> _heartObjects;

    public int GetAmountOfHearts => _heartObjects.Count;
    public HealthBarController(GameObject heartPrefab)
    {
        _heartObjects = new Stack<GameObject>();
        _heartPrefab = heartPrefab;
    }

    public void UpdateHealthBar(int newHealth, GameObject uiObject)
    {
        if (newHealth < 0) throw new ArgumentOutOfRangeException();
        if (newHealth > _heartObjects.Count)
        {
            for (int i = _heartObjects.Count; i < newHealth; i++)
            {
                GameObject heartObject = GameObject.Instantiate(_heartPrefab, uiObject.transform);
                Vector3 location = new Vector3(5 + i * 15, -5 + ((i%2)* -5), 0);
                heartObject.transform.SetLocalPositionAndRotation(location, new Quaternion());
                _heartObjects.Push(heartObject);
            }
        }
        else if(newHealth < _heartObjects.Count)
        {
            if(_heartObjects.Count == 0)
                return;
            for (int i = _heartObjects.Count; i > newHealth; i--)
            {
                GameObject heart = _heartObjects.Pop();
                GameObject.Destroy(heart);
            }
        }
    }
}
