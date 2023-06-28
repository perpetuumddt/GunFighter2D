using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperienceBarUIController : MonoBehaviour
{
    private ExperienceBarController _experienceBarController;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private TMP_Text _levelText;
    void Awake()
    {
        _experienceBarController = new ExperienceBarController(_progressBar, _levelText);
    }

    private void OnEnable()
    {
        // Debug.Log(_playerController.PlayerLevelController);
        // _playerController.PlayerLevelController.OnExperienceChange += _experienceBarController.HandleExperienceChange;
    }

    private void OnDisable()
    {
        // _playerController.PlayerLevelController.OnExperienceChange -= _experienceBarController.HandleExperienceChange;
    }

    private void Start()
    {
        _playerController.PlayerLevelController.OnExperienceChange += _experienceBarController.HandleExperienceChange;
        _playerController.PlayerLevelController.OnLevelUp += _experienceBarController.HandleLevelUp;
    }
}
