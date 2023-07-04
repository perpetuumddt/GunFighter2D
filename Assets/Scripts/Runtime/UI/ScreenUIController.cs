using UnityEngine;
using UnityEngine.UI;

namespace Gunfighter.Runtime.UI
{
    public class ScreenUIController : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        protected void SetActive(bool isActive)
        {
            canvasGroup.alpha = isActive ? 1 : 0;
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }
    }
}
