using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackObjectScript : MonoBehaviour
{
    private Health playerHealth;
    private Rigidbody rb;
    private float timer;
    [SerializeField] private float damage = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<Health>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
