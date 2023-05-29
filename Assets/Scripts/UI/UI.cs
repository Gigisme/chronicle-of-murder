using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI ammoText;
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    
    private void FormatAmmoText(int loaded, int total)
    {
        ammoText.text = loaded + "/" + total;
    }

    public void SetAmmo(int loaded, int total)
    {
        FormatAmmoText(loaded, total);
    }

}
