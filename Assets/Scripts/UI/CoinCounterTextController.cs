using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CoinCounterTextController : MonoBehaviour
    {
        [SerializeField]
        private ScriptableObjectIntVariable _coinCounter;

        [SerializeField]
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            ChangeText(_coinCounter.Variable);
        }
        private void OnEnable()
        {
            _coinCounter.OnVariableChanged += ChangeText;
        }

        private void OnDisable()
        {
            _coinCounter.OnVariableChanged -= ChangeText;
        }

        private void ChangeText(int NewCoinCounter)
        {
            _textMeshPro.text = NewCoinCounter.ToString();
        }
    }
}
