using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackObjectScript : MonoBehaviour
{
    private Health playerHealth;
    private Rigidbody rb;
    private float timer;
    [SerializeField] public float damage = 2f;
    [SerializeField] private float duration;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
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
