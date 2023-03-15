using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletNumberScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text number;


    void Update()
    {
        float bulletNumber = player.GetComponent<Weapon>().attackNumber;
        number.text = "Bullets: " + bulletNumber;

    }
}
