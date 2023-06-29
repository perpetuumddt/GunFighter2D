using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Data/Event/Two Int Event Channel")]
public class ScriptableObjectTwoIntEvent : ScriptableObject
{
    public UnityAction<int,int> OnEventRaised;

    public void RaiseEvent(int value1,int value2)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value1,value2);
        }
    }
}