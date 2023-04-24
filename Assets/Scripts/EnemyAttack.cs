using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform attackObjectSpawn;
    [SerializeField] private GameObject attackObjectPrefab;
    [SerializeField] private float attackObjectSpeed = 5f;
    [SerializeField] private float timer = 2;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;
            attack();
        }

    }
        
    void attack()
    {
        var attackObject = Instantiate(attackObjectPrefab, attackObjectSpawn.position, attackObjectSpawn.rotation);
        attackObject.GetComponent<Rigidbody>().velocity = attackObjectSpawn.forward * attackObjectSpeed;
    }
}
