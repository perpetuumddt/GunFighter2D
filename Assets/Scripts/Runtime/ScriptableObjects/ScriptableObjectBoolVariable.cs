using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Bool Variable", menuName = "Data/Variables/New Bool Variable")]

    public class ScriptableObjectBoolVariable : ScriptableObject
    {
        public event Action<bool> OnVariableChanged;

        [FormerlySerializedAs("_variable")] [SerializeField]
        private bool variable;

        public bool Variable => variable;

        public void ChangeVariable(bool variable)
        {
            this.variable = variable;
            OnVariableChanged?.Invoke(variable);
        }
    }
}