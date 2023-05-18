using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class CoinController : MonoBehaviour, IDetectable, ICollectable, IDrop
{
    [SerializeField]
    private ScriptableObjectIntVariable _playerCoinCounter; 

    [SerializeField]
    private ScriptableObjectIntVariable _coinAmount;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private ParticleSystem _collectedPS;

    public event ObjectDetectedHandler OnObjectDetectedEvent;
    public event ObjectDetectedHandler OnObjectDetectedReleasedEvent;

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
        _playerCoinCounter.IncreaseVariable(_coinAmount.Variable);
        _spriteRenderer.enabled = false;
        _collectedPS.Play();
        await Task.Delay(1000);
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        PlayerCollectorController.OnPlayerPositionUpdate -= MoveTowardsDetector;
    }

    private void MoveTowardsDetector(Vector2 moveDestination)
    {
        transform.position = Vector2.MoveTowards(transform.position, moveDestination, 8 * Time.deltaTime);
    }
}
