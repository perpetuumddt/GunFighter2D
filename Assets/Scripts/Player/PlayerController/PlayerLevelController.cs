using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelController
{
    private PlayerData _playerData;
    private int _level;
    private int _experience;
    public event Action<int> OnLevelUp;
    public int Level
    {
        get => _level;
    }

    public PlayerLevelController(PlayerData playerData, int level, int exp)
    {
        _playerData = playerData;
        _level = level;
        _experience = exp;
    }
    private void LevelUp()
    {
        _level++;
        _experience = 0;
        OnLevelUp?.Invoke(Level);
    }

    public void AddExperience(int addExp)
    {
        if (addExp < 0) throw new ArgumentOutOfRangeException();
        int absoluteExperienceToCurrentLvl = (int)_playerData.ExperienceLevelDistribution.Evaluate(Level);
        int absoluteExperienceToNextLvl = (int)_playerData.ExperienceLevelDistribution.Evaluate(Level + 1);
        int experienceToNextLvl = absoluteExperienceToNextLvl - (absoluteExperienceToCurrentLvl + _experience);
        Debug.Log("addExp " + addExp + " ETCL: " + absoluteExperienceToCurrentLvl + " ETNL: " +experienceToNextLvl + " Level: " + _level);
        if (addExp > experienceToNextLvl)
        {
            LevelUp();
            AddExperience(addExp-experienceToNextLvl);
        }
        else
        {
            _experience += addExp;
        }
        Debug.Log("Level: " + _level + " xp: " + _experience);
    }

    
    
}
