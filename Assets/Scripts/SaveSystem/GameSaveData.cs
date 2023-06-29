using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    [SerializeField]
    private int _playerLevel;
    public int PlayerLevel
    {
        get => _playerLevel;
        set => _playerLevel = value;
    }

    [SerializeField]
    private int _playerExperience;
    public int PlayerExperience
    {
        get => _playerExperience;
        set => _playerExperience = value;
    }
    
    public GameSaveData()
    {
        _playerLevel = 1;
        _playerExperience = 0;
    }
    

}
