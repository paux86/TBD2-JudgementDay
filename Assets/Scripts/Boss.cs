﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] int maxHealth = 500;
    public bool isEnraged;
    public bool isInvulnerable = false;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        isEnraged = false;
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        if (!isEnraged && health <= (maxHealth / 2))
        {
            isEnraged = true;
            GetComponent<Animator>().SetBool("Enraged", true);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}