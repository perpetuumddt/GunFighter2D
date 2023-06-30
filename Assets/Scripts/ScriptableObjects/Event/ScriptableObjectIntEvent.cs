using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Event
{
    [CreateAssetMenu(menuName = "Data/Event/Int Event Channel")]
    public class ScriptableObjectIntEvent : ScriptableObject
    {
        public UnityAction<int> OnEventRaised;

        public void RaiseEvent(int value)
        {
            if (OnEventRaised != null)
            {
                OnEventRaised.Invoke(value);
            }
        }
    }
}