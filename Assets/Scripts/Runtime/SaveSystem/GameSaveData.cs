using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.SaveSystem
{
    [System.Serializable]
    public class GameSaveData
    {
        [SerializeField]
        private  PlayerSaveData playerSaveData;
        public PlayerSaveData PlayerSaveData => playerSaveData;

        public GameSaveData()
        {
            playerSaveData = new PlayerSaveData();
        }
    

    }
}
