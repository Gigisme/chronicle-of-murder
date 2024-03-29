using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private UI ui;
    private int currentHealth;



    private void Start()
    {
        currentHealth = startingHealth;
        ui.SetMaxHealth(startingHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        ui.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            GetComponent<Respawn>().Spawn();
            currentHealth = startingHealth;
            ui.SetMaxHealth(startingHealth);
            GetComponent<Weapon>().ResetAmmo();
        }
    }
    
    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, startingHealth);
        ui.SetHealth(currentHealth);
    }
    
    public void UpgradeHealth(int amount)
    {
        startingHealth += amount;
        currentHealth += amount;
        ui.SetMaxHealth(currentHealth);
    }
}
