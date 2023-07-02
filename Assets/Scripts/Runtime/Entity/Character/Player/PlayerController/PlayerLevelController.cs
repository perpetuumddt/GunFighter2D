using System;
using Gunfighter.Runtime.ScriptableObjects.Data.Character.Player;
using Gunfighter.Runtime.ScriptableObjects.Event;
using UnityEngine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Player.PlayerController
{
    public class PlayerLevelController: MonoBehaviour
    {
        [SerializeField] private ScriptableObjectExpEvent expIncomingChannel;
        [SerializeField] private ScriptableObjectTwoIntEvent onExpChangedChannel; 
        [SerializeField] private ScriptableObjectIntEvent onLevelUpChannel;

        private CharacterController _characterController;
        private PlayerData _playerData;
        private int _level;
        private int _experience;
        public event Action<int> OnLevelUp;
        public event Action<int,int> OnExperienceChange;
        public int Level => _level;
        public int Experience => _experience;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            
            OnLevelUp += PrintLvl;
        }

        private void Start()
        {
            _playerData = (PlayerData)_characterController.CharacterData;
            SetLevelAndExperience(1,0);
        }

        private void OnEnable()
        {
            expIncomingChannel.EventRaised += AddExperience;
            OnExperienceChange += onExpChangedChannel.RaiseEvent;
            OnLevelUp += onLevelUpChannel.RaiseEvent;
        }

        private void OnDisable()
        {
            expIncomingChannel.EventRaised -= AddExperience;
            OnExperienceChange -= onExpChangedChannel.RaiseEvent;
            OnLevelUp -= onLevelUpChannel.RaiseEvent;
        }

        public void PrintLvl(int lvl)
        {
            Debug.Log("lvl: " + lvl);
        }
        private void LevelUp()
        {
            _level++;
            _experience = 0;
            OnLevelUp?.Invoke(Level);
        }

        public void SetLevelAndExperience(int level, int experience)
        {
            if (level < 1 || experience < 0) throw new ArgumentOutOfRangeException();
            _level = level - 1;
            LevelUp();// Calls levelUp event only once
            AddExperience(experience);
        }

        public void AddExperience(int addExp)
        {
            if (addExp < 0) throw new ArgumentOutOfRangeException();
            var experienceToNextLvl = ExperienceToNextLvl;
            if (experienceToNextLvl <= 0) throw new OverflowException("Experience to next level is zero or negative!");
            if (addExp >= experienceToNextLvl)
            {
                LevelUp();
                AddExperience(addExp-experienceToNextLvl);
            }
            else
            {
                _experience += addExp;
            }
            OnExperienceChange?.Invoke(Experience,ExperienceToNextLvl);
        }

        public int ExperienceToNextLvl
        {
            get
            {
                int absoluteExperienceToCurrentLvl = (int)_playerData.ExperienceLevelDistribution.Evaluate(Level);
                int absoluteExperienceToNextLvl = (int)_playerData.ExperienceLevelDistribution.Evaluate(Level + 1);
                return absoluteExperienceToNextLvl - (absoluteExperienceToCurrentLvl + _experience);
            }
        }

        public void SetPlayerData(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}
