using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private void ResumeGame(bool isPause)
    {
        Time.timeScale = isPause ? 1f : 0f;
    }
}
