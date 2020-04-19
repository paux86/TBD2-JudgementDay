﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour,TakeDamageInterface
{
    [SerializeField] int maxHealth = 500;
    private int health;
    GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        gameState = FindObjectOfType<GameState>().GetComponent<GameState>();
    }

    public void TakeDamage(int damage)
    {
        
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("BossDeath");
        animator.tag = "Untargetable";
        StartNewMap();
    }

    private void StartNewMap()
    {
        gameState.StartNewMap();
    }
}