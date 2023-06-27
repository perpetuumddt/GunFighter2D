using System;
using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
using UnityEngine.TextCore.Text;

public class PlayerController : CharacterController
{
    [SerializeField] private EnemySpawner _spawner;
    private PlayerLevelController _levelController;
    private void Awake()
    {
        _stateMachine = new StateMachine<CharacterController>();
        _stateMachine.CurrentState = new PlayerIdleState(this, _stateMachine);
        _stateMachine.CurrentState.Initialize();
        _levelController = new PlayerLevelController((PlayerData)this.CharacterData, 1, 0);
        _levelController.AddExperience(15);
        _levelController.AddExperience(45);
        _levelController.AddExperience(30);
        _levelController.AddExperience(666);
    }

    
}
