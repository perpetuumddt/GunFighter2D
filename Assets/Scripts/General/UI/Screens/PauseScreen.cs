using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : ScreenUIController
{
    [SerializeField]
    private Button _exitPauseButton;
    [SerializeField]
    private ScriptableObjectBoolVariable _isExitPause;

    public void OnEnable()
    {
        _exitPauseButton.onClick.AddListener(DeactivatePause);
        _isExitPause.OnVariableChanged += SetActive;
    }

    public void OnDisable()
    {
        _exitPauseButton.onClick.RemoveListener(DeactivatePause);
        _isExitPause.OnVariableChanged -= SetActive;
    }

    private void DeactivatePause()
    {
        _isExitPause.ChangeVariable(true);
    }
}
