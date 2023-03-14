using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    public void Spawn()
    {
        transform.position = respawnPoint.transform.position;
        Physics.SyncTransforms();
    }
}
