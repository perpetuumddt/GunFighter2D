using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class CoinController : MonoBehaviour, IDetectable, ICollectable
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
        StartCoroutine(MoveTowardsDetector(detectionSource.transform.position));
    }

    public void DetectionReleased(GameObject detectionSource)
    {
        StopAllCoroutines();
    }

    public async void DoCollect()
    {
        _playerCoinCounter.IncreaseVariable(_coinAmount.Variable);
        _spriteRenderer.enabled = false;
        _collectedPS.Play();
        await Task.Delay(1000);
        Destroy(gameObject);
    }

    private IEnumerator MoveTowardsDetector(Vector3 moveDestination)
    {
        float elapsedTime = 0f;
        float travelTime = 5f;
        while(elapsedTime < travelTime) 
        {
            transform.position = Vector3.Lerp(transform.position, moveDestination, elapsedTime/travelTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
