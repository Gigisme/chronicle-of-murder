using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private GameObject upgradesMenuUI;
    [SerializeField] private int damageUpgrade;
    [SerializeField] private float speedUpgrade;
    [SerializeField] private int healthUpgrade;
    private bool cursorLocked = true;
    private void Start()
    {
        upgradesMenuUI.SetActive(false);
    }

    public void LevelUp()
    {
        ToggleCursor();
        upgradesMenuUI.SetActive(true);
    }

    public void AttackDamageOption()
    {
        GetComponent<Weapon>().UpgradeAttackDamage(damageUpgrade);
        upgradesMenuUI.SetActive(false);
        ToggleCursor();
    }
    
    public void AttackSpeedOption()
    {
        GetComponent<Weapon>().UpgradeAttackSpeed(speedUpgrade);
        upgradesMenuUI.SetActive(false);
        ToggleCursor();
    }
    
    public void HealthOption()
    {
        GetComponent<PlayerHealth>().UpgradeHealth(healthUpgrade);
        upgradesMenuUI.SetActive(false);
        ToggleCursor();
    }
    
    private void ToggleCursor()
    {
        cursorLocked = !cursorLocked;
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
