using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] HealthBar healthBar;

    private void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    public void TakeDamage(int attackDamage)
    {
        health -= attackDamage;
        healthBar.SetHealth(health);
    }
}
