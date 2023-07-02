using Gunfighter.Runtime.Entity.Character.Player.PlayerController;
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
