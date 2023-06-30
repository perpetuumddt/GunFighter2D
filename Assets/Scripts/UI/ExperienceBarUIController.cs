using Gunfighter.ScriptableObjects.Event;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.UI
{
    public class ExperienceBarUIController : MonoBehaviour
    {
        private ExperienceBarController _experienceBarController;
        [FormerlySerializedAs("_progressBar")] [SerializeField] private ProgressBar progressBar;
        [FormerlySerializedAs("_levelText")] [SerializeField] private TMP_Text levelText;
        [FormerlySerializedAs("_onXpChangedChannel")] [SerializeField] private ScriptableObjectTwoIntEvent onXpChangedChannel;
        [FormerlySerializedAs("_onLevelUpChannel")] [SerializeField] private ScriptableObjectIntEvent onLevelUpChannel;
        void Awake()
        {
            _experienceBarController = new ExperienceBarController(progressBar, levelText);
        }

        private void OnEnable()
        {
            onXpChangedChannel.EventRaised += _experienceBarController.HandleExperienceChange;
            onLevelUpChannel.EventRaised += _experienceBarController.HandleLevelUp;
        }

        private void OnDisable()
        {
            onXpChangedChannel.EventRaised -= _experienceBarController.HandleExperienceChange;
            onLevelUpChannel.EventRaised -= _experienceBarController.HandleLevelUp;
        }
    }
}
