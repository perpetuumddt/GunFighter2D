using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperienceBarController
{
    private ProgressBar _progressBar;
    private TMP_Text _levelText;
    public ExperienceBarController(ProgressBar progressBar, TMP_Text levelText)
    {
        _progressBar = progressBar;
        _levelText = levelText;
    }
    public void HandleExperienceChange(int experience, int experienceToNextLevel)
    {
        _progressBar.SetCurrentFill(0,experienceToNextLevel+experience,experience);
    }

    public void HandleLevelUp(int level)
    {
        _levelText.SetText($"{level} LVL");
    }
}
