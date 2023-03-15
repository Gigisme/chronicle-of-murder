using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private float currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        if (currentHealth <= 0)
        {
            GetComponent<Respawn>().Spawn();
            currentHealth = startingHealth;
        }
        else
        {
            Debug.Log("Current health: " + currentHealth);
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }

}
