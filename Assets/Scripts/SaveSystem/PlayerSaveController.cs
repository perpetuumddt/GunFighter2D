using Gunfighter.Entity.Character.Player.PlayerController;
using Gunfighter.Interface.SaveSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.SaveSystem
{
    public class PlayerSaveController : MonoBehaviour, IDataPersistence
    {
        [FormerlySerializedAs("_playerController")] [SerializeField] private PlayerController playerController;
    
    
        public void Save(ref GameSaveData gameSaveData)
        {
            gameSaveData.PlayerSaveData.PlayerLevel = playerController.PlayerLevelController.Level;
            gameSaveData.PlayerSaveData.PlayerExperience = playerController.PlayerLevelController.Experience;
            gameSaveData.PlayerSaveData.PlayerCurrentHp = playerController.CharacterHealthController.CurrentHealth;
            gameSaveData.PlayerSaveData.PlayerMaxHp = playerController.CharacterHealthController.MaxHealth;

        }

        public void Load(GameSaveData gameSaveData)
        {
            playerController.PlayerLevelController.SetLevelAndExperience(gameSaveData.PlayerSaveData.PlayerLevel,gameSaveData.PlayerSaveData.PlayerExperience);
            playerController.CharacterHealthController.ChangeMaxHealth(gameSaveData.PlayerSaveData.PlayerMaxHp);
            playerController.CharacterHealthController.CurrentHealth = gameSaveData.PlayerSaveData.PlayerCurrentHp;
        }
    }
}
