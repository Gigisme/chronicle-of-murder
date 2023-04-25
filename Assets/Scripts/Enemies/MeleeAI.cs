using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAI : EnemyAI
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
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
        Debug.Log("Attacking");
        if (isTriggered)
        {
            player.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
