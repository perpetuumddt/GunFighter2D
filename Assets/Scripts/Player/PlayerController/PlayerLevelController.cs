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
    public event Action<int,int> OnExperienceChange;
    public int Level => _level;
    public int Experience => _experience;

    public PlayerLevelController(PlayerData playerData, int level, int exp)
    {
        _playerData = playerData;
        _level = level;
        _experience = exp;
        OnLevelUp += PrintLvl;
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
}
