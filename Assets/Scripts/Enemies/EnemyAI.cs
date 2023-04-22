using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    private EnemyState state;
    private bool canAttack;
    [SerializeField] public Collider attackCollider;
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;
    [SerializeField] public GameObject player;
    [SerializeField] private bool canPatrol;
    [SerializeField] private float attackCooldown;
    
    protected virtual void Start()
    {
        state = canPatrol ? EnemyState.Patrol : EnemyState.Idle;
        player = GameObject.FindWithTag("Player");
        canAttack = true;
    }

    protected virtual void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= chaseRange && distanceToPlayer > attackRange)
        {
            state = EnemyState.Chase;
        }
        else if (distanceToPlayer <= attackRange)
        {
            state = EnemyState.Attack;
        }
        else 
            state = EnemyState.Idle;
        
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chase:
                // Look at player
                transform.LookAt(player.transform);
                Chase();
                break;
            case EnemyState.Attack:
                if (canAttack)
                {
                    // Look at player
                    transform.LookAt(player.transform);
                    Attack();
                    StartCoroutine(AttackCooldown());
                }
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    protected virtual void Chase() {}
    protected virtual void Attack() {}
    protected virtual void Idle() {}
    protected virtual void Patrol() {}
}
