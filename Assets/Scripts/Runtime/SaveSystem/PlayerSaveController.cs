using Gunfighter.Runtime.Entity.Character.Player.Controllers;
using Gunfighter.Runtime.Interface.SaveSystem;
using UnityEngine;

namespace Gunfighter.Runtime.SaveSystem
{
    public class PlayerSaveController : MonoBehaviour, IDataPersistence
    {
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        public void Save(ref GameSaveData gameSaveData)
        {
            gameSaveData.PlayerSaveData.PlayerLevel = _playerController.LevelController.Level;
            gameSaveData.PlayerSaveData.PlayerExperience = _playerController.LevelController.Experience;
            gameSaveData.PlayerSaveData.PlayerCurrentHp = _playerController.HealthController.CurrentHealth;
            gameSaveData.PlayerSaveData.PlayerMaxHp = _playerController.HealthController.MaxHealth;

        }

        public void Load(GameSaveData gameSaveData)
        {
            _playerController.LevelController.SetLevelAndExperience(gameSaveData.PlayerSaveData.PlayerLevel,gameSaveData.PlayerSaveData.PlayerExperience);
            _playerController.HealthController.ChangeMaxHealth(gameSaveData.PlayerSaveData.PlayerMaxHp);
            _playerController.HealthController.CurrentHealth = gameSaveData.PlayerSaveData.PlayerCurrentHp;
        }
    }
}
