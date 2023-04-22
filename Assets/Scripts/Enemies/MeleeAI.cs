using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : EnemyAI
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private bool isTriggered;

    protected override void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }

    protected override void Attack()
    {
        Debug.Log("attacking");
        if (isTriggered)
        {
            player.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
