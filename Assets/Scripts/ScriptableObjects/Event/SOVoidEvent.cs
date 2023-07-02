using UnityEngine;
using UnityEngine.Events;

namespace Gunfighter.ScriptableObjects.Event
{
    [CreateAssetMenu(menuName = "Data/Event/Void Event Channel")]
    public class SOVoidEvent : ScriptableObject
    {
        public UnityAction EventRaised;

        public void RaiseEvent()
        {
            EventRaised?.Invoke();
        }
    }
}