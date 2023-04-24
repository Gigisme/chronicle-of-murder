using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletNumberScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text number;
    private Weapon weapon;

    private void Awake()
    {
        weapon = player.GetComponent<Weapon>();
    }

    private void Update()
    {
        number.text = "Bullets: " + weapon.AttackNumber();
    }
}
