using System;
using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
using UnityEngine.TextCore.Text;

public class PlayerController : CharacterController
{
    private PlayerLevelController _playerLevelController;
    public PlayerLevelController PlayerLevelController => _playerLevelController;
    [SerializeField] private ScriptableObjectExpEvent _expChannel;
    private void Awake()
    {
        _playerLevelController = new PlayerLevelController((PlayerData)this.CharacterData, 1, 0);
        _stateMachine = new StateMachine<CharacterController>();
        _stateMachine.CurrentState = new PlayerIdleState(this, _stateMachine);
        _stateMachine.CurrentState.Initialize();
    }

    private void OnEnable()
    {
        _expChannel.OnEventRaised += _playerLevelController.AddExperience;
    }

    private void OnDisable()
    {
        _expChannel.OnEventRaised -= _playerLevelController.AddExperience;
    }
}
