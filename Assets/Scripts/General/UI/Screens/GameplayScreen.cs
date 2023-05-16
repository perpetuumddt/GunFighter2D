using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScreen : ScreenUIController
{
    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private ScriptableObjectBoolVariable _isPause;

    public void OnEnable()
    {
        _pauseButton.onClick.AddListener(ActivatePause);
        _isPause.OnVariableChanged += OnPause;
    }

    public void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(ActivatePause);
        _isPause.OnVariableChanged -= OnPause;
    }

    private void ActivatePause()
    {
        _isPause.ChangeVariable(true);
    }

    private void OnPause(bool isPause)
    {
        SetActive(!isPause);
    }
}
