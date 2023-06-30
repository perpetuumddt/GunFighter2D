using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Int Variable", menuName = "Data/Variables/New Int Variable")]

    public class ScriptableObjectIntVariable : ScriptableObject
    {

        public event Action<int> OnVariableChanged;

        [SerializeField]
        private int _variable;
        [SerializeField]
        private string _valueName;

        public int Variable => _variable;

        public void ChangeVariable(int variable)
        {
            _variable = variable;
            OnVariableChanged?.Invoke(variable);
            PlayerPrefs.SetInt(_valueName, variable);
        }

        public void IncreaseVariable(int value)
        {
            ChangeVariable(_variable+value);
            OnVariableChanged?.Invoke(_variable);
        }

        public void DecreaseVariable(int value) 
        {
            ChangeVariable(_variable - value);
            OnVariableChanged?.Invoke(_variable);
        }

        public void RestoreValue()
        {
            _variable = PlayerPrefs.GetInt(_valueName);
        }
    }
}
