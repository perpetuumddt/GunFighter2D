using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Int Variable", menuName = "Data/Variables/New Int Variable")]

    public class ScriptableObjectIntVariable : ScriptableObject
    {

        public event Action<int> OnVariableChanged;

        [FormerlySerializedAs("_variable")] [SerializeField]
        private int variable;
        [FormerlySerializedAs("_valueName")] [SerializeField]
        private string valueName;

        public int Variable => variable;

        public void ChangeVariable(int variable)
        {
            this.variable = variable;
            OnVariableChanged?.Invoke(variable);
            PlayerPrefs.SetInt(valueName, variable);
        }

        public void IncreaseVariable(int value)
        {
            ChangeVariable(variable+value);
            OnVariableChanged?.Invoke(variable);
        }

        public void DecreaseVariable(int value) 
        {
            ChangeVariable(variable - value);
            OnVariableChanged?.Invoke(variable);
        }

        public void RestoreValue()
        {
            variable = PlayerPrefs.GetInt(valueName);
        }
    }
}
