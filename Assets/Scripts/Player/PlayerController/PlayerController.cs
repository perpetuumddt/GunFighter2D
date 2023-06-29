using System;
using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEditor.VersionControl.Asset;
using UnityEngine.TextCore.Text;

public class PlayerController : CharacterController
{
    private PlayerLevelController _playerLevelController;
    public PlayerLevelController PlayerLevelController => _playerLevelController;
    [SerializeField] private ScriptableObjectExpEvent _expIncomingChannel;
    [SerializeField] private ScriptableObjectTwoIntEvent _onExpChangedChannel; 
    [SerializeField] private ScriptableObjectIntEvent _onLevelUpChannel; 
    private void Awake()
    {
        _playerLevelController = new PlayerLevelController((PlayerData)this.CharacterData, 1, 0);
        _stateMachine = new StateMachine<CharacterController>();
        _stateMachine.CurrentState = new PlayerIdleState(this, _stateMachine);
        _stateMachine.CurrentState.Initialize();
    }

    private void OnEnable()
    {
        _expIncomingChannel.OnEventRaised += _playerLevelController.AddExperience;
        _playerLevelController.OnExperienceChange += _onExpChangedChannel.RaiseEvent;
        _playerLevelController.OnLevelUp += _onLevelUpChannel.RaiseEvent;
    }

    private void OnDisable()
    {
        _expIncomingChannel.OnEventRaised -= _playerLevelController.AddExperience;
        _playerLevelController.OnExperienceChange -= _onExpChangedChannel.RaiseEvent;
        _playerLevelController.OnLevelUp -= _onLevelUpChannel.RaiseEvent;
    }
}
