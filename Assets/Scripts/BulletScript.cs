using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);

        if (other.gameObject.CompareTag("Enemy"))
        {
            //other.gameObject.GetComponent<enemyHealth>().health -= 20;
            //Destroy(other.gameObject);
        }
    }
}
