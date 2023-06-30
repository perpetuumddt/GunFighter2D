using Entity.Character.Controller;
using Entity.Character.Player.PlayerController;
using ScriptableObjects.Data.Character.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.Character.Enemy.EnemyController
{
    public class EnemyMovementController : CharacterMovementController
    {
        [SerializeField]
        private Transform targetPositionTransform;

        [SerializeField]
        private EnemyData _enemyData;

        private NavMeshAgent agent;

        private bool _isMoving;
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        private void OnEnable()
        {
            targetPositionTransform = GameObject.FindObjectOfType<PlayerController>().transform;
        }

        private void Update()
        {
            if(_isMoving)
            {
                agent.speed = _enemyData.MovementSpeed;
                agent.destination = targetPositionTransform.position;
            }
            else
            {
                agent.isStopped = true;
            }
        }

        public override void DoMove(params object[] param)
        {
            _isMoving = (bool)param[0];
        }
    }
}
