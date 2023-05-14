using UnityEditor;
using UnityEngine;

public interface IDetectable
{
    event ObjectDetectedHandler OnObjectDetectedEvent;
    event ObjectDetectedHandler OnObjectDetectedReleasedEvent;

    GameObject gameObject { get;}

    void Detected(GameObject detectionSource);
    void DetectionReleased(GameObject detectionSource);
}