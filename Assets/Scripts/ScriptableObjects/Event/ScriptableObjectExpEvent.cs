using Gunfighter.ScriptableObjects.Data.Character;
using Gunfighter.ScriptableObjects.Data.Character.Enemies;
using UnityEngine;
using UnityEngine.Events;
using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.ScriptableObjects.Event
{
    [CreateAssetMenu(menuName = "Data/Event/Exp Event Channel")]
    public class ScriptableObjectExpEvent : ScriptableObject
    {
        public UnityAction<int> EventRaised;

        public void RaiseEvent(GameObject obj)
        {
            CharacterData charData = obj.GetComponent<CharacterController>().CharacterData;
            int exp = ((EnemyData)charData).BaseXpReward;
            if (EventRaised != null)
            {
                EventRaised.Invoke(exp);
            }
        }
    }
}
