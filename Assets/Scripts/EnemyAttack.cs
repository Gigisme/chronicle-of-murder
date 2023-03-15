using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackObjectSpawn;
    public GameObject attackObjectPrefab;
    public float attackObjectSpeed = 5f;
    private float timer;
    private GameObject player;
    public float range = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if (distance < range) 
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                attack();
            }
        }
        
    }

    void attack()
    {
        Instantiate(attackObjectPrefab, attackObjectSpawn.position, Quaternion.identity);
    }
}
