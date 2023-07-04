using Gunfighter.Runtime.ScriptableObjects.Event;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gunfighter.Runtime.UI.Screens
{
    public class DeathScreen : ScreenUIController
    {
        [SerializeField]
        private Button titleScreenButton;
        [SerializeField]
        private Button newGameButton;

        [SerializeField] private SOVoidEvent deathEvent;

        [SerializeField] 
        private Camera _camera;
        [SerializeField] 
        private Animator _screenShadeAnim;
        private void OnEnable()
        {
            deathEvent.EventRaised += ActivateDeathScreen;
            titleScreenButton.onClick.AddListener(TitleScreen);
        }

        private void OnDisable()
        {
            deathEvent.EventRaised -= ActivateDeathScreen;
        }

        private static void TitleScreen()
        {
            SceneManager.LoadScene(0);
        }

        private void ActivateDeathScreen()
        {
            SetActive(true);
        }

        private async void ActivateDeathScene()
        {
            
        }
    }
}
