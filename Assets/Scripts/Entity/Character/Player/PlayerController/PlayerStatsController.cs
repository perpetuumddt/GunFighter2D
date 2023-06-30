using Gunfighter.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Player.PlayerController
{
    public class PlayerStatsController : MonoBehaviour
    {
        [FormerlySerializedAs("_playerCoinCounter")] [SerializeField]
        private ScriptableObjectIntVariable playerCoinCounter;
        void Awake()
        {
            //_playerCoinCounter.RestoreValue(); //äëÿ èçóìðóäîâ
            playerCoinCounter.ChangeVariable(0);
        }
    }
}
