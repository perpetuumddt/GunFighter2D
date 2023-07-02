using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.SaveSystem
{
    [System.Serializable]
    public class PlayerSaveData
    {
        [SerializeField] private int playerLevel;
        [SerializeField] private int playerExperience;
        [SerializeField] private int playerMaxHp;
        [SerializeField] private int playerCurrentHp;
        [SerializeField] private int playerCoins;

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