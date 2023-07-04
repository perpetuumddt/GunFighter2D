using System;
using System.Collections;
using System.Collections.Generic;
using Gunfighter.Runtime.Entity.Character.Controller;
using UnityEngine;
using UnityEngine.AI;

namespace Gunfighter.Runtime.Entity.Character.Enemy.EnemyController

{
    public class EnemyRotationController : CharacterRotationController
    {
        private NavMeshAgent _agent;
        private bool _moveLeft;
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public override void CheckLookingDirection()
        {
            if (!_moveLeft && transform.position.x < _agent.destination.x)
            {
                Flip();
            }
            else if (_moveLeft && transform.position.x > _agent.destination.x)
            {
                Flip();
            }
        }

        public override Vector2 CheckMovementDirection()
        {
            return _agent.destination;
        }


        public override void CheckRollingDirection()
        {
            throw new System.NotImplementedException();
        }

        protected override void Flip()
        {
            _moveLeft = !_moveLeft;
            transform.Rotate(0, 180, 0);
        }
    }
}
