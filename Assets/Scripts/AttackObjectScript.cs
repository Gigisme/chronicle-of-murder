using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObjectScript : MonoBehaviour
{
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }
    /**private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);

        if(collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
        }
    }**/
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.gameObject.CompareTag("Target"))
        {
            //other.gameObject.GetComponent<enemyHealth>().health -= 20;
            Destroy(other.gameObject);
        }
    }
}
