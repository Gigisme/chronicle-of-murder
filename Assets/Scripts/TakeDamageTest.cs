using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageTest : Interactable
{
   [SerializeField] private GameObject player;

protected override void Interact()
{
   player.GetComponent<Health>().TakeDamage(20);
}
}
