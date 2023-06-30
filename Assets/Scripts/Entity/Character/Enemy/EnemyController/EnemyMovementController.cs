using Gunfighter.Entity.Character.Controller;
using Gunfighter.Entity.Character.Player.PlayerController;
using Gunfighter.ScriptableObjects.Data.Character.Enemies;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Enemy.EnemyController
{
    public class EnemyMovementController : CharacterMovementController
    {
        [SerializeField]
        private Transform targetPositionTransform;

        [FormerlySerializedAs("_enemyData")] [SerializeField]
        private EnemyData enemyData;

        private NavMeshAgent _agent;

        private bool _isMoving;
        private void Start()
        {
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
