using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform attackObjectSpawn;
    [SerializeField] private GameObject attackObjectPrefab;
    [SerializeField] private float attackObjectSpeed = 5f;
    [SerializeField] private int maxLoadedAmmo;
    [SerializeField] private int maxTotalAmmo;
    [SerializeField] private float reloadTime;
    [SerializeField] private UI ui;
    private int loadedAmmo;
    private int totalAmmo;
    private float attackTime = 0;
    private bool isReloading = false;
    

    private void Start()
    {
        loadedAmmo = maxLoadedAmmo;
        totalAmmo = maxTotalAmmo;
        ui.SetAmmo(loadedAmmo, totalAmmo);
    }
   
    private void Update()
    {
        if (attackTime <= 0 && Time.timeScale != 0)
        {
            if (isReloading)
            {
                return;
            }
            if (loadedAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
            if (Input.GetKey(KeyCode.Mouse0) && loadedAmmo > 0)
            {
                var attackObject = Instantiate(attackObjectPrefab, attackObjectSpawn.position, attackObjectSpawn.rotation);
                attackObject.GetComponent<Rigidbody>().velocity = attackObjectSpawn.forward * attackObjectSpeed;
                loadedAmmo--;
                attackTime = 0.5f;
            }
            ui.SetAmmo(loadedAmmo, totalAmmo);
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        loadedAmmo = maxLoadedAmmo;
        totalAmmo -= maxLoadedAmmo;
    }
}
