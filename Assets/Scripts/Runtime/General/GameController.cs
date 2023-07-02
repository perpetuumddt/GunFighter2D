using Gunfighter.Runtime.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.General
{
    public class GameController : MonoBehaviour
    {
        [FormerlySerializedAs("_isPause")] [SerializeField]
        private ScriptableObjectBoolVariable isPause;

        private void OnEnable()
        {
            isPause.OnVariableChanged += PauseGame;
        }

        private void OnDisable()
        {
            isPause.OnVariableChanged -= PauseGame;
        }

        private void PauseGame(bool isPause)
        {
            Time.timeScale = isPause ? 0f : 1f;
        }
    }
}
