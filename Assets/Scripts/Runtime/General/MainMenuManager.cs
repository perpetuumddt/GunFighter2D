using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gunfighter.Runtime.General
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] 
        private Button _playButton;

        [SerializeField] 
        private Animator _screenFadeAnimator;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(StartNewGame);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(StartNewGame);
        }

        private async void StartNewGame()
        {
            ScreenFadeIn();
            await Task.Delay(2000);
            SceneManager.LoadScene(1);
        }

        private void ScreenFadeIn()
        {
            _screenFadeAnimator.SetTrigger("ScreenFadeIn");
        }
    }
}
