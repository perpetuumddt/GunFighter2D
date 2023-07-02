using System;
using Gunfighter.ScriptableObjects.Event;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gunfighter.UI.Screens
{
    public class DeathScreen : ScreenUIController
    {
        [SerializeField]
        private Button titleScreenButton;
        [SerializeField]
        private Button newGameButton;

        [SerializeField] private SOVoidEvent deathEvent;

        private void OnEnable()
        {
            deathEvent.EventRaised += ActivateDeathScreen;
            titleScreenButton.onClick.AddListener(TitleScreen);
        }

        private void OnDisable()
        {
            deathEvent.EventRaised -= ActivateDeathScreen;
        }

        private void TitleScreen()
        {
            
            SceneManager.LoadScene(0);
        }

        private void ActivateDeathScreen()
        {
            SetActive(true);
        }
    }
}
