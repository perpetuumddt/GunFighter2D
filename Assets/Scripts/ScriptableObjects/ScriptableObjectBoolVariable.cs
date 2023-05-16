using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bool Variable", menuName = "Data/Variables/New Bool Variable")]

public class ScriptableObjectBoolVariable : ScriptableObject
{
    public event Action<bool> OnVariableChanged;

    [SerializeField]
    private bool _variable;

    public bool Variable => _variable;

    public void ChangeVariable(bool variable)
    {
        _variable = variable;
        OnVariableChanged?.Invoke(variable);
    }
}