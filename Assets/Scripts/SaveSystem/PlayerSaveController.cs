using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveController : MonoBehaviour, IDataPersistence
{
    [SerializeField] private PlayerController _playerController;
    
    
    public void Save(ref GameSaveData gameSaveData)
    {
        gameSaveData.PlayerSaveData.PlayerLevel = _playerController.PlayerLevelController.Level;
        gameSaveData.PlayerSaveData.PlayerExperience = _playerController.PlayerLevelController.Experience;
        gameSaveData.PlayerSaveData.PlayerCurrentHp = _playerController.CharacterHealthController.CurrentHealth;

    }

    public void Load(GameSaveData gameSaveData)
    {
        _playerController.PlayerLevelController.SetLevelAndExperience(gameSaveData.PlayerSaveData.PlayerLevel,gameSaveData.PlayerSaveData.PlayerExperience);
        _playerController.CharacterHealthController.CurrentHealth = gameSaveData.PlayerSaveData.PlayerCurrentHp;
    }
}
