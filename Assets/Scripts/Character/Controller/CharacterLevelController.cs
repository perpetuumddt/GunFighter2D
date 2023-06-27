using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevelController
{
    private PlayerData _playerData;
    private int _level;
    private int _experience;
    public event Action<int> OnLevelUp;
    public int Level
    {
        get => _level;
    }

    public CharacterLevelController(PlayerData playerData, int level, int exp)
    {
        _playerData = playerData;
        _level = level;
        _experience = exp;
    }
    private void LevelUp()
    {
        _level++;
        OnLevelUp?.Invoke(Level);
    }

    public void AddExperience(int addExp)
    {
        if (addExp < 0) throw new ArgumentOutOfRangeException();
        int absoluteExperienceToCurrentLvl = (int)_playerData.ExperienceLevelDistribution.Evaluate(Level);
        int absoluteExperienceToNextLvl = (int)_playerData.ExperienceLevelDistribution.Evaluate(Level + 1);
        int experienceToNextLvl = absoluteExperienceToCurrentLvl + _experience - absoluteExperienceToNextLvl;
        if (addExp > experienceToNextLvl)
        {
            LevelUp();
            AddExperience(addExp-experienceToNextLvl);
        }
        else
        {
            _experience += addExp;
        }

    }

    
    
}
