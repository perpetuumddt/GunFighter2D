using UnityEngine;
using UnityEngine.Events;

namespace Gunfighter.ScriptableObjects.Event
{
    [CreateAssetMenu(menuName = "Data/Event/Int Event Channel")]
    public class ScriptableObjectIntEvent : ScriptableObject
    {
        public UnityAction<int> EventRaised;

        public void RaiseEvent(int value)
        {
            if (EventRaised != null)
            {
                EventRaised.Invoke(value);
            }
        }
    }
}