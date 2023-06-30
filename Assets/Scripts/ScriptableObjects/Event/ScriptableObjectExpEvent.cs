using ScriptableObjects.Data.Character;
using ScriptableObjects.Data.Character.Enemies;
using UnityEngine;
using UnityEngine.Events;
using CharacterController = Entity.Character.Controller.CharacterController;

namespace ScriptableObjects.Event
{
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
}
