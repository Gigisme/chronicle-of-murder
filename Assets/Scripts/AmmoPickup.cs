using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoAmount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<Weapon>().IsFullAmmo())
                return;
            other.GetComponent<Weapon>().AddAmmo(ammoAmount);
            Destroy(gameObject);
        }
    }
}
