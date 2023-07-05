using Gunfighter.Runtime.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.Entity.Character.Player.Controllers
{
    public class PlayerStatsController : MonoBehaviour
    {
        [FormerlySerializedAs("_playerCoinCounter")] [SerializeField]
        private ScriptableObjectIntVariable playerCoinCounter;
        void Awake()
        {
            //_playerCoinCounter.RestoreValue(); //äëÿ èçóìðóäîâ - что за хуйня?
            playerCoinCounter.ChangeVariable(0);
        }
    }
}
