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
    [SerializeField] Animator animator;
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
                animator.SetBool("Shooting", false);
                StartCoroutine(Reload());
                return;
            }
            if (Input.GetKey(KeyCode.R) && loadedAmmo < maxLoadedAmmo && totalAmmo > 0)
            {
                animator.SetBool("Shooting", false);
                StartCoroutine(Reload());
                return;
            }
            if (Input.GetKey(KeyCode.Mouse0) && loadedAmmo > 0)
            {
                animator.SetBool("Shooting", true);
                var attackObject = Instantiate(attackObjectPrefab, attackObjectSpawn.position, attackObjectSpawn.rotation);
                attackObject.GetComponent<Rigidbody>().velocity = attackObjectSpawn.forward * attackObjectSpeed;
                loadedAmmo--;
                attackTime = 0.5f;
            }
            else
            {
                animator.SetBool("Shooting", false);
            }
            ui.SetAmmo(loadedAmmo, totalAmmo);
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }
    
    public void ResetAmmo()
    {
        loadedAmmo = maxLoadedAmmo;
        totalAmmo = maxTotalAmmo;
        ui.SetAmmo(loadedAmmo, totalAmmo);
    }
    
    public void AddAmmo(int ammoAmount)
    {
        totalAmmo = Mathf.Clamp(totalAmmo + ammoAmount, 0, maxTotalAmmo);
        ui.SetAmmo(loadedAmmo, totalAmmo);
    }
    
    public bool IsFullAmmo()
    {
        return totalAmmo == maxTotalAmmo;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        isReloading = false;
        
        if (loadedAmmo > 0)
        {
            totalAmmo -= maxLoadedAmmo - loadedAmmo;
            loadedAmmo = maxLoadedAmmo;
        }
        else
        {
            loadedAmmo = maxLoadedAmmo;
            totalAmmo -= maxLoadedAmmo;
        }
        
    }
}
