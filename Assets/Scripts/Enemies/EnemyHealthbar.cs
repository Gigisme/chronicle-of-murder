using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] private Image enemyHealthBar;

    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    public void UpdateHealthbar(float maxHealth, float currentHealth)
    {
        enemyHealthBar.fillAmount = currentHealth / maxHealth;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
    }
}
