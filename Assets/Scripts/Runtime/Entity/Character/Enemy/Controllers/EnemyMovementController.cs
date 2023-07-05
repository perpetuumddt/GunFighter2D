using Gunfighter.Runtime.Entity.Character.Controllers;
using Gunfighter.Runtime.Entity.Character.Player.Controllers;
using Gunfighter.Runtime.ScriptableObjects.Data.Entity.Character.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace Gunfighter.Runtime.Entity.Character.Enemy.Controllers
{
    public class EnemyMovementController : CharacterMovementController
    {
        [SerializeField]
        private Transform targetPositionTransform;
        
        private EnemyData _enemyData;
        private NavMeshAgent _agent;
        private bool _isMoving;
        private void Start()
        {
            _enemyData = (EnemyData)characterController.CharacterData;
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void OnEnable()
        {
            targetPositionTransform = FindObjectOfType<PlayerController>().transform;
        }

        private void Update()
        {
            if(_isMoving)
            {
                _agent.speed = _enemyData.MovementSpeed;
                _agent.destination = targetPositionTransform.position;
            }
            else
            {
                _agent.isStopped = true;
            }
        }

        public override void DoMove(params object[] param)
        {
            _isMoving = (bool)param[0];
        }
    }
}
