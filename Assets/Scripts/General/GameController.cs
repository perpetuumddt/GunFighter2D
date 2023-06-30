using ScriptableObjects;
using UnityEngine;

namespace General
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private ScriptableObjectBoolVariable _isPause;

        private void OnEnable()
        {
            _isPause.OnVariableChanged += PauseGame;
        }

        private void OnDisable()
        {
            _isPause.OnVariableChanged -= PauseGame;
        }

        private void PauseGame(bool isPause)
        {
            Time.timeScale = isPause ? 0f : 1f;
        }
    }
}
