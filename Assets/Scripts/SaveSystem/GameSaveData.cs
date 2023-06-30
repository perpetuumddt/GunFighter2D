using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    [SerializeField]
    private  PlayerSaveData _playerSaveData;
    public PlayerSaveData PlayerSaveData => _playerSaveData;

    public GameSaveData()
    {
        _playerSaveData = new PlayerSaveData();
    }
    

}
