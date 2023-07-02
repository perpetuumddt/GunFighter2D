using Gunfighter.Runtime.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.UI
{
    public class CoinCounterTextController : MonoBehaviour
    {
        [FormerlySerializedAs("_coinCounter")] [SerializeField]
        private ScriptableObjectIntVariable coinCounter;

        [FormerlySerializedAs("_textMeshPro")] [SerializeField]
        private TextMeshProUGUI textMeshPro;

        private void Awake()
        {
            ChangeText(coinCounter.Variable);
        }
        private void OnEnable()
        {
            coinCounter.OnVariableChanged += ChangeText;
        }

        private void OnDisable()
        {
            coinCounter.OnVariableChanged -= ChangeText;
        }

        private void ChangeText(int newCoinCounter)
        {
            textMeshPro.text = newCoinCounter.ToString();
        }
    }
}
