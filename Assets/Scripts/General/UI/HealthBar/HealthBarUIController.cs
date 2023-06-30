using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIController : MonoBehaviour
{
    [SerializeField] 
    private GameObject _heartPrefab;
    
    [SerializeField]
    private PlayerHealthController _playerHealthController;

    private HealthBarController _healthBarController;
    private void Awake()
    {
        _healthBarController = new HealthBarController(_heartPrefab,this.gameObject);
    }

    private void OnEnable()
    {
        UpdateHealthBar(_playerHealthController._playerData.DefaultMaxHealth);
        _playerHealthController.OnUpdateHealth += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _playerHealthController.OnUpdateHealth -= UpdateHealthBar;
    }

    public void UpdateHealthBar(int _currentHealth)
    {
        _healthBarController.SetupDisplay(_playerHealthController.MaxHealth);
        _healthBarController.UpdateDisplay(_currentHealth);
    }
}