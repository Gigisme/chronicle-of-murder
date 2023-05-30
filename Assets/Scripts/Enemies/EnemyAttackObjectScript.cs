using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackObjectScript : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Rigidbody rb;
    private float timer;
    [SerializeField] public int damage = 2;
    [SerializeField] private float duration = 2;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > duration)
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
