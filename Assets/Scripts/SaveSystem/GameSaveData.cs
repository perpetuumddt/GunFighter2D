using UnityEngine;

namespace SaveSystem
{
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
}
