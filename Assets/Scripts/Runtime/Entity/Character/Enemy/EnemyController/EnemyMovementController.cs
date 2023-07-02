using Gunfighter.Runtime.Entity.Character.Controller;
using Gunfighter.Runtime.Entity.Character.Player.PlayerController;
using Gunfighter.Runtime.ScriptableObjects.Data.Character.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace Gunfighter.Runtime.Entity.Character.Enemy.EnemyController
{
    public class EnemyMovementController : CharacterMovementController
    {
        [SerializeField]
        private Transform targetPositionTransform;

        
        private EnemyData enemyData;

        private NavMeshAgent _agent;

        private bool _isMoving;
        private void Start()
        {
            enemyData = (EnemyData)characterController.CharacterData;
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void OnEnable()
        {
            targetPositionTransform = GameObject.FindObjectOfType<PlayerController>().transform;
        }

        private void Update()
        {
            if(_isMoving)
            {
                _agent.speed = enemyData.MovementSpeed;
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