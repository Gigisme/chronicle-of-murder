using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform attackObjectSpawn;
    public GameObject attackObjectPrefab;
    public float attackObjectSpeed = 5f;
    public float attackNumber = 5;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackNumber > 0)
        {
            var attackObject = Instantiate(attackObjectPrefab, attackObjectSpawn.position, attackObjectSpawn.rotation);
            attackObject.GetComponent<Rigidbody>().velocity = attackObjectSpawn.forward * attackObjectSpeed;
            attackNumber--;
        }
    }
}
