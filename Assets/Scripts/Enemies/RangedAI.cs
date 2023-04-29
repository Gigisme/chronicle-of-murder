using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


public class RangedAI : EnemyAI
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private Transform attackObjectSpawn;
    [SerializeField] private GameObject attackObject;
    [SerializeField] private float attackObjectSpeed;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = moveSpeed;
    }

    protected override void Attack()
    {
        _navMeshAgent.isStopped = true;
        
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(attackObjectSpawn.forward, directionToPlayer);
        Vector3 up = Vector3.Cross(attackObjectSpawn.forward, directionToPlayer);

        var attackObject = Instantiate(this.attackObject, attackObjectSpawn.position, attackObjectSpawn.rotation);
        attackObject.GetComponent<EnemyAttackObjectScript>().damage = damage;
        attackObject.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(angle, up) * attackObjectSpawn.forward * attackObjectSpeed;
    }
    
    protected override void Chase()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(player.transform.position);
    }
}
