﻿using System;
using UnityEditor;
using UnityEngine;

public class CharacterData : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private int _health;
    public int Health => _health;

    [SerializeField]
    private float _movementSpeed;
    public float MovementSpeed => _movementSpeed;
    
    public event Action<CharacterData> OnDeath;

    public void InvokeOnDeath(CharacterData data)
    {
        OnDeath?.Invoke(data);
    }
}