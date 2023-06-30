using ScriptableObjects.Event;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ExperienceBarUIController : MonoBehaviour
    {
        private ExperienceBarController _experienceBarController;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private ScriptableObjectTwoIntEvent _onXpChangedChannel;
        [SerializeField] private ScriptableObjectIntEvent _onLevelUpChannel;
        void Awake()
        {
            _experienceBarController = new ExperienceBarController(_progressBar, _levelText);
        }

        private void OnEnable()
        {
            _onXpChangedChannel.OnEventRaised += _experienceBarController.HandleExperienceChange;
            _onLevelUpChannel.OnEventRaised += _experienceBarController.HandleLevelUp;
        }

        private void OnDisable()
        {
            _onXpChangedChannel.OnEventRaised -= _experienceBarController.HandleExperienceChange;
            _onLevelUpChannel.OnEventRaised -= _experienceBarController.HandleLevelUp;
        }
    }
}
