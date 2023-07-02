using UnityEngine;

namespace Gunfighter.Runtime.Interface.Detect
{
    public delegate void ObjectDetectedHandler(GameObject sourse, GameObject detectedObject);

    public interface IDetector
    {
        event ObjectDetectedHandler OnObjectDetectedEvent;
        event ObjectDetectedHandler OnObjectDetectioReleasedEvent;

        void Detect(IDetectable detectableObject);
        void Detect(GameObject detectedObject);

        void ReleaseDetection(IDetectable detectableObject);
        void ReleaseDetection(GameObject detectedObject);
    }
}