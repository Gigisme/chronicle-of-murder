using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5;
    public float currentHealth;
    [SerializeField] private EnemyHealthbar healthBar;
    [SerializeField] private int experienceWorth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthbar(maxHealth, currentHealth);
    }

    void Update()
    {
        if (currentHealth <= 0) 
        {
            GetComponentInParent<EnemyAI>().player.GetComponentInChildren<Experience>().GainExperience(experienceWorth);
            Destroy(gameObject);
        }
        else
        {
            healthBar.UpdateHealthbar(maxHealth, currentHealth);
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= GetComponentInParent<EnemyAI>().player.GetComponentInChildren<Weapon>().GetDamage();
        }
    }
}
