using System.Threading.Tasks;
using Gunfighter.Entity.Character.Player.PlayerController;
using Gunfighter.Interface.Collect;
using Gunfighter.Interface.Detect;
using Gunfighter.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.General.Drop
{
    public class CoinController : MonoBehaviour, IDetectable, ICollectable
    {
        [FormerlySerializedAs("_playerCoinCounter")] [SerializeField]
        private ScriptableObjectIntVariable playerCoinCounter; 

        [FormerlySerializedAs("_coinAmount")] [SerializeField]
        private ScriptableObjectIntVariable coinAmount;

        [FormerlySerializedAs("_spriteRenderer")] [SerializeField]
        private SpriteRenderer spriteRenderer;

        [FormerlySerializedAs("_collectedPS")] [SerializeField]
        private ParticleSystem collectedPS;

        [FormerlySerializedAs("CoinsPool")] [SerializeField]
        private Transform coinsPool;

        //public event ObjectDetectedHandler OnObjectDetectedEvent;
        //public event ObjectDetectedHandler OnObjectDetectedReleasedEvent;

        public GameObject GameObject { get; }

        public void Detected(GameObject detectionSource)
        {
            PlayerCollectorController.OnPlayerPositionUpdate += MoveTowardsDetector;
        }

        public void DetectionReleased(GameObject detectionSource)
        {
            PlayerCollectorController.OnPlayerPositionUpdate -= MoveTowardsDetector;
        }

        public async void DoCollect()
        {
            playerCoinCounter.IncreaseVariable(coinAmount.Variable);
            spriteRenderer.enabled = false;
            collectedPS.Play();
            await Task.Delay(1000);
            if(this != null)
                this.gameObject.SetActive(false);
        }

        private void MoveTowardsDetector(Vector2 moveDestination)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveDestination, 8 * Time.deltaTime);
        }
    }
}
