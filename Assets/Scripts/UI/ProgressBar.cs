using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Gunfighter.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [FormerlySerializedAs("_minimum")] [SerializeField] private int minimum;
        [FormerlySerializedAs("_maximum")] [SerializeField] private int maximum;
        [FormerlySerializedAs("_current")] [SerializeField] private int current;
        [FormerlySerializedAs("_slider")] [SerializeField] private Slider slider;
        void Awake()
        {
        
        }

        public void SetCurrentFill(int min, int max, int current)
        {
            slider.maxValue = max;
            slider.minValue = min;
            slider.value = current;
        }
    }
}
