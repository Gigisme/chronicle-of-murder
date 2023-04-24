using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform attackObjectSpawn;
    [SerializeField] private GameObject attackObjectPrefab;
    [SerializeField] private float attackObjectSpeed = 5f;
    [SerializeField] private float attackNumber = 5;
    private float attackTime = 0;
   
    private void Update()
    {
        if (attackTime <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0) && attackNumber > 0)
            {
                var attackObject = Instantiate(attackObjectPrefab, attackObjectSpawn.position, attackObjectSpawn.rotation);
                attackObject.GetComponent<Rigidbody>().velocity = attackObjectSpawn.forward * attackObjectSpeed;
                attackNumber--;
                attackTime = 0.5f;
            }
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }

    public float AttackNumber() 
    {
        return attackNumber;
    }
}
