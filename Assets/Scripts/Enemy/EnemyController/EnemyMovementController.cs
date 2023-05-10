using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : CharacterMovementController
{
    [SerializeField]
    private Transform targetPositionTransform;

    private NavMeshAgent agent;

    private bool _isMoving;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if(_isMoving)
        {
            agent.destination = targetPositionTransform.position;
        }
        else
        {
            agent.Stop();
        }
    }

    public override void DoMove(params object[] param)
    {
        _isMoving = (bool)param[0];
    }
}
