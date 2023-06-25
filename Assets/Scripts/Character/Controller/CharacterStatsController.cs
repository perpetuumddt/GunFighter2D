using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsController : MonoBehaviour
{
    [SerializeField]
    private CharacterData _characterData;
    public CharacterData CharacterData => _characterData;

    public float Health => _characterData.Health;
    public float MovementSpeed => _characterData.MovementSpeed;


    public virtual void UpdateStats()
    {
        
    }
}
