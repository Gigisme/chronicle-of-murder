using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAI : EnemyAI
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private bool isTriggered;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = speed;
    }

    protected override void Chase()
    {
        _navMeshAgent.SetDestination(player.transform.position);
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
        _navMeshAgent.SetDestination(transform.position); // Stop moving
        if (isTriggered)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
