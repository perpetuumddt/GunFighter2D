using UnityEngine;

namespace Gunfighter.Runtime.Interface.Detect
{
    public interface IDetectable
    {
        //event ObjectDetectedHandler OnObjectDetectedEvent;
        //event ObjectDetectedHandler OnObjectDetectedReleasedEvent;

        GameObject GameObject { get;}

        void Detected(GameObject detectionSource);
        void DetectionReleased(GameObject detectionSource);
    }
}