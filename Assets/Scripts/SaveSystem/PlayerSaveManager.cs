using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private PlayerController _playerController;
    
    
    public void Save(ref GameSaveData gameSaveData)
    {
        gameSaveData.PlayerLevel = _playerController.PlayerLevelController.Level;
        gameSaveData.PlayerExperience = _playerController.PlayerLevelController.Experience;
    }

    public void Load(GameSaveData gameSaveData)
    {
        _playerController.PlayerLevelController.SetLevelAndExperience(gameSaveData.PlayerLevel,gameSaveData.PlayerExperience);
    }
}
