using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.SaveSystem
{
    [System.Serializable]
    public class PlayerSaveData
    {
        [FormerlySerializedAs("_playerLevel")] [SerializeField] private int playerLevel;
        [FormerlySerializedAs("_playerExperience")] [SerializeField] private int playerExperience;
        [FormerlySerializedAs("_playerMaxHP")] [SerializeField] private int playerMaxHp;
        [FormerlySerializedAs("_playerCurrentHP")] [SerializeField] private int playerCurrentHp;
        [FormerlySerializedAs("_playerCoins")] [SerializeField] private int playerCoins;

        public PlayerSaveData()
        {
            playerLevel = 1;
            playerExperience = 0;
            playerMaxHp = 6;
            playerCurrentHp = 6;
            playerCoins = 0;
        }

        public int PlayerLevel
        {
            get => playerLevel;
            set => playerLevel = value;
        }

        public int PlayerExperience
        {
            get => playerExperience;
            set => playerExperience = value;
        }

        public int PlayerMaxHp
        {
            get => playerMaxHp;
            set => playerMaxHp = value;
        }

        public int PlayerCurrentHp
        {
            get => playerCurrentHp;
            set => playerCurrentHp = value;
        }

        public int PlayerCoins
        {
            get => playerCoins;
            set => playerCoins = value;
        }
    }
}