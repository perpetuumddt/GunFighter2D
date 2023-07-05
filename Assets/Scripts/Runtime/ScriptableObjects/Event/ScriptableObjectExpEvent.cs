using Gunfighter.Runtime.ScriptableObjects.Data.Entity.Character;
using Gunfighter.Runtime.ScriptableObjects.Data.Entity.Character.Enemies;
using UnityEngine;
using UnityEngine.Events;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controllers.CharacterController;

namespace Gunfighter.Runtime.ScriptableObjects.Event
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
