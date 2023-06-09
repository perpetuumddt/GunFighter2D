using Gunfighter.Runtime.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Gunfighter.Runtime.UI.Screens
{
    public class PauseScreen : ScreenUIController
    {
        [SerializeField]
        private Button exitPauseButton;
        [SerializeField]
        private ScriptableObjectBoolVariable isPause;

        public void OnEnable()
        {
            exitPauseButton.onClick.AddListener(DeactivatePause);
            isPause.OnVariableChanged += SetActive;
        }

        public void OnDisable()
        {
            exitPauseButton.onClick.RemoveListener(DeactivatePause);
            isPause.OnVariableChanged -= SetActive;
        }

        private void DeactivatePause()
        {
            isPause.ChangeVariable(false);
        }

        private void OnResume(bool isPause)
        {
            SetActive(isPause);
        }
    }
}
