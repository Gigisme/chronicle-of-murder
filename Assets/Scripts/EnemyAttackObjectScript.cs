using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackObjectScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    public float attackSpeed = 5f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.forward;
        rb.velocity = new Vector2(direction.x, direction.z).normalized * attackSpeed;
        
        float rot = Mathf.Atan2(-direction.z, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, rot, 0);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<playerHealth>().health -= 20;
           
        }
    }
}
