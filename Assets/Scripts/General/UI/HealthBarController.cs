using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] 
    private GameObject _heartPrefab;
    
    [SerializeField]
    private PlayerHealthController _playerHealthController;

    private Stack<GameObject> _heartObjects;

    private void Awake()
    {
        _heartObjects = new Stack<GameObject>();
    }

    private void OnEnable()
    {
        UpdateHealthBar(_playerHealthController._playerData.Health);
        _playerHealthController.OnUpdateHealth += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _playerHealthController.OnUpdateHealth -= UpdateHealthBar;
    }

    public void UpdateHealthBar(int _currentHealth)
    {
        if (_currentHealth > _heartObjects.Count)
        {
            for (int i = _heartObjects.Count; i < _currentHealth; i++)
            {
                GameObject heartObject = Instantiate(_heartPrefab, this.transform);
                Vector3 location = new Vector3(5 + i * 15, -5 + ((i%2)* -5), 0);
                heartObject.transform.SetLocalPositionAndRotation(location, new Quaternion());
                _heartObjects.Push(heartObject);
            }
        }
        else if(_currentHealth < _heartObjects.Count)
        {
            if(_heartObjects.Count == 0)
                return;
            for (int i = _heartObjects.Count; i > _currentHealth; i--)
            {
                GameObject heart = _heartObjects.Pop();
                Destroy(heart);
            }
        }
    }
}
