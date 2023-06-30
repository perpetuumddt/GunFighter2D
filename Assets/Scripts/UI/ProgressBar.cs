using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private int _minimum;
        [SerializeField] private int _maximum;
        [SerializeField] private int _current;
        [SerializeField] private Slider _slider;
        void Awake()
        {
        
        }

        public void SetCurrentFill(int min, int max, int current)
        {
            _slider.maxValue = max;
            _slider.minValue = min;
            _slider.value = current;
        }
    }
}
