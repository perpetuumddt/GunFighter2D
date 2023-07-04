using System.Collections;
using System.Threading.Tasks;
using Gunfighter.Runtime.General.CustomYieldInstructions;
using Gunfighter.Runtime.ScriptableObjects.Event;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gunfighter.Runtime.UI.Screens
{
    public class DeathScreen : ScreenUIController
    {
        [SerializeField] private GameObject menu;
        [SerializeField]
        private Button titleScreenButton;
        [SerializeField]
        private Button newGameButton;

        [SerializeField] private SOVoidEvent deathEvent;

        [SerializeField] 
        private Camera _camera;
        [SerializeField] 
        private Animator _screenShadeAnim;

        private bool isActive = false;
        private static readonly int DeathScreenShadeIn = Animator.StringToHash("DeathScreenShadeIn");

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
            SwitchComponentsVisibility(isActive);
            PlayDeathScene();
        }

        private void PlayDeathScene()
        {
            _screenShadeAnim.SetTrigger(DeathScreenShadeIn);
            StartCoroutine(WaitForScreenShadeAnimation());
        }
        
        private IEnumerator WaitForScreenShadeAnimation()
        {
            //yield return new WaitForAnimationToFinish(_screenShadeAnim); //3f
            yield return new WaitForSeconds(4f);
            SwitchComponentsVisibility(isActive);
        }
        
        private void SwitchComponentsVisibility(bool isActive)
        {
            menu.SetActive(isActive);
            this.isActive = !isActive;
        }
    }
}
