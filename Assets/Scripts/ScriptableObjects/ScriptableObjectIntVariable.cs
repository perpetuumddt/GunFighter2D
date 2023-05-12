using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Int Variable", menuName = "Data/Variables/New Int Variable")]

public class ScriptableObjectIntVariable : ScriptableObject
{

    public event Action<int> OnVariableChanged;

    [SerializeField]
    private int _variable;

    public int Variable => _variable;

    public void ChangeVariable(int variable)
    {
        _variable = variable;
        OnVariableChanged?.Invoke(variable);
    }
    //Increase/Decrease Variable methods
}
