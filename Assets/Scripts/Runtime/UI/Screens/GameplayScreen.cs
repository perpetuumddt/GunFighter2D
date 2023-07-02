using Gunfighter.Runtime.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Gunfighter.Runtime.UI.Screens
{
    public class GameplayScreen : ScreenUIController
    {
        [FormerlySerializedAs("_pauseButton")] [SerializeField]
        private Button pauseButton;
        [FormerlySerializedAs("_isPause")] [SerializeField]
        private ScriptableObjectBoolVariable isPause;

        public void OnEnable()
        {
            pauseButton.onClick.AddListener(ActivatePause);
            isPause.OnVariableChanged += OnPause;
        }

        public void OnDisable()
        {
            pauseButton.onClick.RemoveListener(ActivatePause);
            isPause.OnVariableChanged -= OnPause;
        }

        private void ActivatePause()
        {
            isPause.ChangeVariable(true);
        }

        private void OnPause(bool isPause)
        {
            SetActive(!isPause);
        }
    }
}
