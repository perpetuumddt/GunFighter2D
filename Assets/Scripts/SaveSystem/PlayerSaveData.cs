using UnityEngine;

namespace SaveSystem
{
    [System.Serializable]
    public class PlayerSaveData
    {
        [SerializeField] private int _playerLevel;
        [SerializeField] private int _playerExperience;
        [SerializeField] private int _playerMaxHP;
        [SerializeField] private int _playerCurrentHP;
        [SerializeField] private int _playerCoins;

        public PlayerSaveData()
        {
            _playerLevel = 1;
            _playerExperience = 0;
            _playerMaxHP = 6;
            _playerCurrentHP = 6;
            _playerCoins = 0;
        }

        public int PlayerLevel
        {
            get => _playerLevel;
            set => _playerLevel = value;
        }

        public int PlayerExperience
        {
            get => _playerExperience;
            set => _playerExperience = value;
        }

        public int PlayerMaxHp
        {
            get => _playerMaxHP;
            set => _playerMaxHP = value;
        }

        public int PlayerCurrentHp
        {
            get => _playerCurrentHP;
            set => _playerCurrentHP = value;
        }

        public int PlayerCoins
        {
            get => _playerCoins;
            set => _playerCoins = value;
        }
    }
}