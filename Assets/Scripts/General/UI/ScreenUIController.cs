using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUIController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    public void SetActive(bool isActive)
    {
        _canvasGroup.alpha = isActive ? 1 : 0;
        _canvasGroup.interactable = isActive;
        _canvasGroup.blocksRaycasts = isActive;
    }
}
