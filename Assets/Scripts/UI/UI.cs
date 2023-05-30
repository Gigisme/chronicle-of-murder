using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private TextMeshProUGUI ammoText;
    
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    
    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }
    
    private void FormatAmmoText(int loaded, int total)
    {
        ammoText.text = loaded + "/" + total;
    }

    public void SetAmmo(int loaded, int total)
    {
        FormatAmmoText(loaded, total);
    }
    
    public void SetMaxExperience(int experience)
    {
        experienceSlider.maxValue = experience;
    }
    
    public void SetExperience(int experience)
    {
        experienceSlider.value = experience;
    }

}
