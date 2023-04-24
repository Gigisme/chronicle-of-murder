using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5;
    public float currentHealth;
    [SerializeField] private EnemyHealthbar healthBar;
    [SerializeField] private float damage = 2f;
   

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthbar(maxHealth, currentHealth);
    }

    void Update()
    {
        if (currentHealth <= 0) 
        {
            Destroy(gameObject, 2);
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
            currentHealth -= damage;
        }
    }
}
