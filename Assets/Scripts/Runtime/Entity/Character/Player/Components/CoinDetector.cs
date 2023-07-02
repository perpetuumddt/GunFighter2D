using System.Collections.Generic;
using Gunfighter.Runtime.Interface.Detect;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Player.Components
{
    public class CoinDetector : MonoBehaviour, IDetector
    {
        public event ObjectDetectedHandler OnObjectDetectedEvent;
        public event ObjectDetectedHandler OnObjectDetectioReleasedEvent;

        private List<GameObject> _detectedCoins = new List<GameObject>();
        public void Detect(IDetectable detectableObject)
        {
            if(!_detectedCoins.Contains(detectableObject.GameObject))
            {
                detectableObject.Detected(gameObject);
                _detectedCoins.Add(detectableObject.GameObject);

                OnObjectDetectedEvent?.Invoke(gameObject, detectableObject.GameObject);
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
            if(_detectedCoins.Contains(detectableObject.GameObject))
            {
                detectableObject.DetectionReleased(gameObject);
                _detectedCoins.Remove(detectableObject.GameObject);

                OnObjectDetectioReleasedEvent?.Invoke(gameObject, detectableObject.GameObject);
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
            if(IsColliderDetectableObject(collision, out var detectedObject))
            {
                Detect(detectedObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (IsColliderDetectableObject(collision, out var detectedObject))
            {
                ReleaseDetection(detectedObject);
            }
        }

        private bool IsColliderDetectableObject(Collider2D collider, out IDetectable detectedObject) //ìåòîä âàëèäàöèè
        {
            detectedObject = collider.GetComponentInParent<IDetectable>();

            return detectedObject != null;
        }
    }
}
