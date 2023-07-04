using Gunfighter.Runtime.Entity.Character.Enemy.Controllers;
using Gunfighter.Runtime.Entity.Character.States;
using Gunfighter.Runtime.Entity.StateMachine;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Enemy.States
{
    public class EnemyAttackState : CharacterAttackState<EnemyController>
    {
        public EnemyAttackState(EnemyController data, StateMachine<EnemyController> stateMachine) : base(data, stateMachine)
        {
            
        }
    }
}
