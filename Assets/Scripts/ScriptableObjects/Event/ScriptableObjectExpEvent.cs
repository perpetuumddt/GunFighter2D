using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Data/Event/Exp Event Channel")]
public class ScriptableObjectExpEvent : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(GameObject obj)
    {
        CharacterData charData = obj.GetComponent<CharacterController>().CharacterData;
        int exp = ((EnemyData)charData).BaseXpReward;
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(exp);
        }
    }
}
