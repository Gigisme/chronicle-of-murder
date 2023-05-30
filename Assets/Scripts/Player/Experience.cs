using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private UI ui;
    [SerializeField] private int tillNextLevel;
    private int currentExperience;
    private GameObject player;

    public void Start()
    {
        currentExperience = 0;
        ui.SetExperience(0);
        ui.SetMaxExperience(tillNextLevel);
        player = GameObject.FindWithTag("Player");
    }
    
    public void GainExperience(int experience)
    {
        currentExperience += experience;
        ui.SetExperience(experience);
        if (currentExperience >= tillNextLevel)
        {
            player.GetComponent<Upgrades>().LevelUp();
            ui.SetExperience(0);
            ui.SetMaxExperience(tillNextLevel);
        }
    }
}
