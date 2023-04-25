using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    private EnemyState state;
    private bool canAttack;
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
                Chase();
                break;
            case EnemyState.Attack:
            {
                // Turn to player
                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 15f);
                
                if (canAttack)
                {
                    Attack();
                    StartCoroutine(AttackCooldown());
                }
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
