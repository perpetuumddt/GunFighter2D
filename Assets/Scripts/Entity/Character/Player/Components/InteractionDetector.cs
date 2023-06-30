using Interface.Detect;
using UnityEngine;

namespace Entity.Character.Player.Components
{
    public class InteractionDetector : MonoBehaviour, IDetector
    {
        public event ObjectDetectedHandler OnObjectDetectedEvent;
        public event ObjectDetectedHandler OnObjectDetectioReleasedEvent;

        public void Detect(IDetectable detectableObject)
        {
            throw new System.NotImplementedException();
        }

        public void Detect(GameObject detectedObject)
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseDetection(IDetectable detectableObject)
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseDetection(GameObject detectedObject)
        {
            throw new System.NotImplementedException();
        }
    }
}
