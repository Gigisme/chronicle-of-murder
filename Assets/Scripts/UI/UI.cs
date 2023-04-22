using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject player;

    private void Start()
    {
        healthText.text = "Health: " + player.GetComponent<Health>().GetHealth();
    }
    private void Update()
    {
        healthText.text = "Health: " + player.GetComponent<Health>().GetHealth();
    }
}
