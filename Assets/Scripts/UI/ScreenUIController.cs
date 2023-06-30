using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.UI
{
    public class ScreenUIController : MonoBehaviour
    {
        [FormerlySerializedAs("_canvasGroup")] [SerializeField]
        private CanvasGroup canvasGroup;

        public void SetActive(bool isActive)
        {
            canvasGroup.alpha = isActive ? 1 : 0;
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }
    }
}
