using UnityEngine;
using UnityEngine.Events;

namespace Gunfighter.Runtime.ScriptableObjects.Event
{
    [CreateAssetMenu(menuName = "Data/Event/Two Int Event Channel")]
    public class ScriptableObjectTwoIntEvent : ScriptableObject
    {
        public UnityAction<int,int> EventRaised;

        public void RaiseEvent(int value1,int value2)
        {
            if (EventRaised != null)
            {
                EventRaised.Invoke(value1,value2);
            }
        }
    }
}