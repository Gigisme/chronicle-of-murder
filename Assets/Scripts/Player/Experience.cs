using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private UI ui;
    [SerializeField] private int tillNextLevel;
    private int currentExperience;

    public void Start()
    {
        currentExperience = 0;
        ui.SetExperience(0);
        ui.SetMaxExperience(tillNextLevel);
    }
    
    public void GainExperience(int experience)
    {
        currentExperience += experience;
        ui.SetExperience(experience);
        if (currentExperience >= tillNextLevel)
        {
            // LevelUp();
            ui.SetExperience(0);
            ui.SetMaxExperience(tillNextLevel);
        }
    }
}
