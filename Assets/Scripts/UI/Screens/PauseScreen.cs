using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class PauseScreen : ScreenUIController
    {
        [SerializeField]
        private Button _exitPauseButton;
        [SerializeField]
        private ScriptableObjectBoolVariable _isPause;

        public void OnEnable()
        {
            _exitPauseButton.onClick.AddListener(DeactivatePause);
            _isPause.OnVariableChanged += SetActive;
        }

        public void OnDisable()
        {
            _exitPauseButton.onClick.RemoveListener(DeactivatePause);
            _isPause.OnVariableChanged -= SetActive;
        }

        private void DeactivatePause()
        {
            _isPause.ChangeVariable(false);
        }

        private void OnResume(bool isPause)
        {
            SetActive(isPause);
        }
    }
}
