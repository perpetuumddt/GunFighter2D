using UnityEditor;
using UnityEngine;

public class CharacterData : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private float _health;
    public float Health => _health;

    [SerializeField]
    private float _movementSpeed;
    public float MovementSpeed => _movementSpeed;
}