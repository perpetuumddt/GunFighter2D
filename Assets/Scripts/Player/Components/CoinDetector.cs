using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDetector : MonoBehaviour, IDetector
{
    public event ObjectDetectedHandler OnObjectDetectedEvent;
    public event ObjectDetectedHandler OnObjectDetectioReleasedEvent;

    private List<GameObject> _detectedCoins = new List<GameObject>();
    public void Detect(IDetectable detectableObject)
    {
        if(!_detectedCoins.Contains(detectableObject.gameObject))
        {
            detectableObject.Detected(gameObject);
            _detectedCoins.Add(detectableObject.gameObject);

            OnObjectDetectedEvent?.Invoke(gameObject, detectableObject.gameObject);
        }
    }

    public void Detect(GameObject detectedObject)
    {
        if(!_detectedCoins.Contains(detectedObject))
        {
            _detectedCoins.Add (detectedObject);

            OnObjectDetectedEvent?.Invoke(gameObject,detectedObject);
        }
    }

    public void ReleaseDetection(IDetectable detectableObject)
    {
        if(_detectedCoins.Contains(detectableObject.gameObject))
        {
            detectableObject.DetectionReleased(gameObject);
            _detectedCoins.Remove(detectableObject.gameObject);

            OnObjectDetectioReleasedEvent?.Invoke(gameObject, detectableObject.gameObject);
        }
    }

    public void ReleaseDetection(GameObject detectedObject)
    {
        if (_detectedCoins.Contains(detectedObject))
        {
            _detectedCoins.Remove(detectedObject);

            OnObjectDetectioReleasedEvent?.Invoke(gameObject, detectedObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isColliderDetectableObject(collision, out var detectedObject))
        {
            Detect(detectedObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isColliderDetectableObject(collision, out var detectedObject))
        {
            ReleaseDetection(detectedObject);
        }
    }

    private bool isColliderDetectableObject(Collider2D collider, out IDetectable detectedObject) //метод валидации
    {
        detectedObject = collider.GetComponentInParent<IDetectable>();

        return detectedObject != null;
    }
}
