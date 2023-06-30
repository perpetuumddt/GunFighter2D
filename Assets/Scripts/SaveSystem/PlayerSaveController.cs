using Entity.Character.Player.PlayerController;
using Interface.SaveSystem;
using UnityEngine;

namespace SaveSystem
{
    public class PlayerSaveController : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private PlayerController _playerController;
    
    
        public void Save(ref GameSaveData gameSaveData)
        {
            gameSaveData.PlayerSaveData.PlayerLevel = _playerController.PlayerLevelController.Level;
            gameSaveData.PlayerSaveData.PlayerExperience = _playerController.PlayerLevelController.Experience;
            gameSaveData.PlayerSaveData.PlayerCurrentHp = _playerController.CharacterHealthController.CurrentHealth;
            gameSaveData.PlayerSaveData.PlayerMaxHp = _playerController.CharacterHealthController.MaxHealth;

        }

        public void Load(GameSaveData gameSaveData)
        {
            _playerController.PlayerLevelController.SetLevelAndExperience(gameSaveData.PlayerSaveData.PlayerLevel,gameSaveData.PlayerSaveData.PlayerExperience);
            _playerController.CharacterHealthController.ChangeMaxHealth(gameSaveData.PlayerSaveData.PlayerMaxHp);
            _playerController.CharacterHealthController.CurrentHealth = gameSaveData.PlayerSaveData.PlayerCurrentHp;
        }
    }
}
