using ScriptableObjects;
using UnityEngine;

namespace Entity.Character.Player.PlayerController
{
    public class PlayerStatsController : MonoBehaviour
    {
        [SerializeField]
        private ScriptableObjectIntVariable _playerCoinCounter;
        void Awake()
        {
            //_playerCoinCounter.RestoreValue(); //äëÿ èçóìðóäîâ
            _playerCoinCounter.ChangeVariable(0);
        }
    }
}
